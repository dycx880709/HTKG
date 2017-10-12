using SC_AnalysisSystem_Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YJYTemplate.Common;

namespace SC_AnalysisSystem.Resources.Controls
{
    /// <summary>
    /// MessageBoxX.xaml 的交互逻辑
    /// </summary>
    public partial class MessageBoxX : WindowBase
    {
        /// <summary>
        /// 结果，用户点击确定Result=true;
        /// </summary>
        public MessageBoxResult Result { get; private set; }

        private static readonly Dictionary<string, Brush> _Brushes = new Dictionary<string, Brush>();

        private MessageBoxX(EnumNotifyType type, string mes, MessageBoxButton btn)
        {
            InitializeComponent();
            txtMessage.Text = mes;
            systemSetting();
            switch (type)
            {
                case EnumNotifyType.Error:
                    ficon.Text = "\xe61c";
                    break;
                case EnumNotifyType.Warning:
                    ficon.Text = "\xe651";
                    break;
                case EnumNotifyType.Info:
                    ficon.Text = "\xe621";
                    break;
                case EnumNotifyType.Question:
                    ficon.Text = "\xe654";
                    OKbtn.Visibility = Visibility.Hidden;
                    OKbtn.IsDefault = false;
                    switch (btn)
                    {
                        case MessageBoxButton.OK:
                            OKbtn.Visibility = Visibility.Visible;
                            break;
                        case MessageBoxButton.OKCancel:
                            twoBtnSp.Visibility = Visibility.Visible;
                            btnOK.IsDefault = true;
                            break;
                        case MessageBoxButton.YesNoCancel:
                            twoBtnSp.Visibility = Visibility.Hidden;
                            threeBtnSp.Visibility = Visibility.Visible;
                            btnYes.IsDefault = true;
                            break;
                        case MessageBoxButton.YesNo:
                            twoBtnSp.Visibility = Visibility.Hidden;
                            threeBtnSp.Visibility = Visibility.Visible;
                            Cancelbtn.Visibility = Visibility.Collapsed;
                            btnYes.IsDefault = true;
                            break;
                    }
                    break;
            }
            this.KeyDown += (o, e) =>
            {
                if (e.Key == Key.Space)
                {
                    if (btn == MessageBoxButton.YesNoCancel || btn == MessageBoxButton.YesNo)
                        Result = MessageBoxResult.Yes;
                    else
                        Result = MessageBoxResult.OK;
                    this.Close();
                }
                else if (e.Key == Key.Escape)
                {
                    if (btn == MessageBoxButton.YesNo)
                        Result = MessageBoxResult.No;
                    else if (btn == MessageBoxButton.OKCancel || btn == MessageBoxButton.YesNoCancel)
                        Result = MessageBoxResult.Cancel;
                    else
                        Result = MessageBoxResult.OK;
                    this.Close();
                }
            };
        }

        /// <summary>
        /// 系统功能键设置
        /// </summary>
        private void systemSetting()
        {
            topGrid.MouseLeftButtonDown += (o, ex) =>
            {
                if (ex.ClickCount == 1)
                    MouseClickHandle(o, ex);
            };
        }

        private void SetForeground(EnumNotifyType type)
        {
            string key = type.ToString() + "Foreground";
            if (!_Brushes.ContainsKey(key))
            {
                var b = TryFindResource(key) as Brush;
                _Brushes.Add(key, b);
            }
            Foreground = _Brushes[key];
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.OK;
            Close();
            e.Handled = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.No;
            Close();
            e.Handled = true;
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Yes;
            Close();
            e.Handled = true;
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.No;
            Close();
            e.Handled = true;
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Cancel;
            Close();
            e.Handled = true;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Cancel;
            Close();
            e.Handled = true;
        }

        #region Public Method
       
        /// <summary>
        /// 提示错误消息
        /// </summary>
        public static void Error(string mes, Window owner = null)
        {
            Show(EnumNotifyType.Error, mes, owner);
        }

        /// <summary>
        /// 提示普通消息
        /// </summary>
        public static void Info(string mes, Window owner = null)
        {
            Show(EnumNotifyType.Info, mes, owner);
        }

        /// <summary>
        /// 提示警告消息
        /// </summary>
        public static void Warning(string mes, Window owner = null)
        {
            Show(EnumNotifyType.Warning, mes, owner);
        }

        /// <summary>
        /// 提示询问消息 默认按钮YES OR NO
        /// </summary>
        public static MessageBoxResult Question(string mes, MessageBoxButton btn = MessageBoxButton.YesNo, Window owner = null)
        {
            return Show(EnumNotifyType.Question, mes, owner, btn);
        }

        /// <summary>
        /// 显示提示消息框
        /// owner指定所属父窗体，默认参数值为null，则指定主窗体为父窗体。
        /// </summary>
        private static MessageBoxResult Show(EnumNotifyType type, string mes, Window owner = null, MessageBoxButton btn = MessageBoxButton.YesNo)
        {
            MessageBoxResult res = MessageBoxResult.None;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                MessageBoxX nb = new MessageBoxX(type, mes, btn) { Title = type.GetDescription() };
                nb.Owner = owner ?? WPFAppUtil.GetTopWindow();
                //nb.Owner = owner ?? Application.Current.MainWindow;
                nb.ShowDialog();
                res = nb.Result;
            }));
            return res;
        }

        /// <summary>
        /// 显示提示消息框
        /// </summary>
        /// <param name="type">对话框类型</param>
        /// <param name="mes">信息</param>
        /// <param name="isShow">是否显示按钮</param>
        /// <param name="owner">指定所属父窗体，默认参数值为null，则指定主窗体为父窗体。</param>
        /// <returns></returns>
        private static MessageBoxResult Show(EnumNotifyType type, string mes, Window owner = null)
        {
            MessageBoxResult res = MessageBoxResult.None;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                MessageBoxX nb = new MessageBoxX(type, mes, MessageBoxButton.OK) { Title = type.GetDescription() };
                nb.Owner = owner ?? WPFAppUtil.GetTopWindow();
                nb.ShowDialog();
                res = nb.Result;
            }));
            return res;
        }
        #endregion

        /// <summary>
        /// 通知消息类型
        /// </summary>
        public enum EnumNotifyType
        {
            [Description("错误信息")]
            Error,
            [Description("警告信息")]
            Warning,
            [Description("系统信息")]
            Info,
            [Description("询问信息")]
            Question,
        }
    }
}
