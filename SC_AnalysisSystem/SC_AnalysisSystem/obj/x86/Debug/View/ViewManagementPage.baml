   M S B A M L   `   `   `  ����L  HSC_AnalysisSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:SC_AnalysisSystem.ViewModelSC_AnalysisSystem.ViewModel  5      /    )SC_AnalysisSystem.View.ViewManagementPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8vm)clr-namespace:SC_AnalysisSystem.ViewModel     5        N� Title$  ViewManagementPage��5       � 	Resources 5	      9   3SC_AnalysisSystem.ViewModel.ViewManagementViewModel   ViewManagementViewModel��%   &	          5
   
   6%   5       � DataContext 5      �� 5   
    �� Source# [  6   .��5      � $��10, 5q�6    � RowDefinitions 5   
   �� 5      $��2* �6   �� 5      $��* �6   .+�5   
   �� $!��正在进行的训练列表��6   .��5      &   System.Windows.Controls.DataGrid  $��5q�6      SelectionMode$ Single=�6"   ��69   �� *Gamers+   	CellStyle 5      �� 5       �� 
TargetType*  $System.Windows.Controls.DataGridCell"  6     BaseOnCellStyle �� BasedOn# [ 66   	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$
 ID��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 65   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6?   �� *
PersonName+  5      $
 游戏类型��6)   $ * 68    6B   �� *
PersonName+  5      $
 开始时间��6)   $ * 68    6B   �� *	IpAddress+  5      $
 场景名称��6)   $ * 68    6B   �� *Delay+  5      $
 当前状态��6)   $ * 68    6B   �� *Delay+  5      $
 玩家人数��6)   $ * 68    6B   �� *Delay+  5       $
 游戏难度��6)   $ * 68    6B   �� *Delay+  5!      $
 建议用时��6)   $ * 68    6B   �� *Delay+5#      5$      �� 5%   
   $��1��6   $��人员配置列表��6    .��5&      �� -   5         ��Name$ person_ListBox��5&      $	��5,0q�6R   ��6.   �� *PersonInfos+��5'      �� 5(      " ��6   .�5)      ��  �� Property � �� �� Value$	 120��63   �� 5*       � �� 5+      �� 5,   "    �� 
TargetType" ��62   .R�5-   &   �� #��z��6-   #��z��6W   #��z��6y   ._�5.   *   �� #��z��6;   #��z��6z   5/   '   50   #   52       �� Triggers 53      55      ��57      �� 58      .R�59      �  � ColumnDefinitions 5:      �� 5;   "   $	��120 �63   �� 5<   "   $	��100 �63   .+�5>      �� $��0��6$   $
��Fill=�6N   ��64   �� *Source+6N   �� 5?      $��1��6)   .+�5@   "   ��  �� Orientation$ 
Horizontal=�6-   .+�5A   &   �� $��用户名: ��60   �� 5B   &   ��60   �� *
UserName+5C   #   �� 5D   "   $ 
Horizontal=�6-   $	��0,5q�6F   .+�5E   &   �� $��角色: ��60   �� 5F   &   ��60   �� *Role+5G   #   �� 5H   "   $ 
Horizontal=�6-   .+�5I   &   �� $��评分: ��60   �� 5J   &   ��60   �� *Source+5K   #   5L      5M      5N      5P      5Q      5R      5S      