   M S B A M L   `   `   `  ����Q  MSC_AnalysisSystem_Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}Sclr-namespace:SC_AnalysisSystem_Core.AttachProperty;assembly=SC_AnalysisSystem_Core%SC_AnalysisSystem_Core.AttachProperty  5   	   L HSC_AnalysisSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:SC_AnalysisSystem.Resources.Controls$SC_AnalysisSystem.Resources.Controls 5   	   6   0SC_AnalysisSystem.Resources.Controls.MessageBoxX   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   fattachSclr-namespace:SC_AnalysisSystem_Core.AttachProperty;assembly=SC_AnalysisSystem_Core     5   	   Dlocal2clr-namespace:SC_AnalysisSystem.Resources.Controls     5   	   -   5        5  /SC_AnalysisSystem.Resources.Controls.WindowBase   Name$  mb��5	   @   $	��240��5   	   $	��360��6    � WindowStyle$
 None=�6"    � WindowStartupLocation$ CenterOwner=�65    � Title$ MessageBoxX��5	   	   $��14(�6   ���#666666 � 	Resources 5
       �� 
TargetType   MessageButtonStyle �� BasedOn��%0   '��       7   �� 5   
   " ��6   8   6-   5      .��5      �  � RowDefinitions 5   
   �� 5      $��32 �6   �� 5      $��* �6   .+�5   
   � -   5         �Name$ topGrid��5      $��0��6    .+�5      �� ���#66c7ff6   �� 5      $	����6   $��12,0,0,1q�6(   $��15(�6:   
������� 	 FIcon#��[ 6[   $
��Left=�6z   �� 5      $��系统信息��6   
�������$��1��67   $
��Left=�6G   $��Center=�6b   $��40,0,0,1q�6}   $��14(�6�   �� 5      -   5        	 ��Name$	 btnClose��5      $��2��6'   
 �� IsCancel
 . 
�������$	����6Z     FIButtonStyle#��[ 6�    � ToolTip$ 关闭��6�   $��Right=�6�   $��13(�6�   $��0,0,10,0q�6�   5      � 5   
   $��1��6    5      �� 5      $��* �6    �� 5      $
��Auto �6    .+�5      �  � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5       $��* �6'   .+�5"      �� -   5         ��Name$ ficon��5"      $y�	Collapsed=�6+   $��65��6B   $��Center=�6M   $��35(�6h   $	����6v   #��[ 6�   ��6�   �� *
Foreground+ �� ElementName$ mb��6�   �� 5#      -   5        $ 
txtMessage��5#      $��1��60   $
��15,5q�6@   $��Center=�6N   $��Center=�6�   $
��Wrap=�5$   E   $��这是一个错误提示��6Y   ��5#   i   �� *
Foreground+$ mb����5$      �� *
FontSize+$ mb��6Y   5%      � 5&      -   5        $ btnGrid��5&      $��1��6#   $��45��60   .+�5'      �� $��1��6   $	��Top=�6$   $��1��6<   ���#eeeeee$��1q�6_   �� 5(      -   5        $	 OKbtn��5(      $��1��6(   $��84��65    �� 	IsDefault . A   ;SC_AnalysisSystem_Core.AttachProperty.ControlAttachProperty   	FIconText$	 ��6Q   .��6�   我知道了6�   �� 5)      -   5         ��Name$ twoBtnSp��5)      $y�Hidden=�6/   $��1��6C    �� Orientation$ 
Horizontal=�6P   $��Center=�6i   $��1q�6�   $��0��6�   $��2��6�   $��Center=�6�   .+�5*      �� -	   5        $	 btnOK��5*      $	 ��6,   $��0,0,20,0q�6^   .��6�   确定6�   �� 5+      -
   5        $	 	btnCancel��5+      $	 ��60   .��6z   取消6~   5,      �� 5-      -   5        $ 
threeBtnSp��5-      $y�Hidden=�61   $��1��6E   $ 
Horizontal=�6R   $��Center=�6k   $��1q�6�   $��0��6�   $��2��6�   $��Center=�6�   .+�5.      �� -   5        $	 btnYes��5.      $	 ��6-   $��0,0,20,0q�6_   .��6�   是6�   �� 5/      -   5        $	 btnNo��5/      $	 ��6,   $��0,0,20,0q�6r   .��6�   否6�   �� 50      -   5        $	 	Cancelbtn��50      $	 ��60   .��6z   取消6~   51      52      53      54      55      