   M S B A M L   `   `   `  ����Q  MSC_AnalysisSystem_Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}Sclr-namespace:SC_AnalysisSystem_Core.AttachProperty;assembly=SC_AnalysisSystem_Core%SC_AnalysisSystem_Core.AttachProperty  5   	   L HSC_AnalysisSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:SC_AnalysisSystem.ViewModelSC_AnalysisSystem.ViewModel 5   	   (   "SC_AnalysisSystem.View.LoginWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   dcoreSclr-namespace:SC_AnalysisSystem_Core.AttachProperty;assembly=SC_AnalysisSystem_Core     5   	   8vm)clr-namespace:SC_AnalysisSystem.ViewModel     5   	     � Title$  登录��5   	   $	��250��6   $	��300��6!    � 	Resources 5      0  *SC_AnalysisSystem.ViewModel.LoginViewModel   LoginViewModel��%   &	          5   
   6   5	       � DataContext 5
      �� 5   
    �� Source# [  6   .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��3* �6   �� 5      $��* �6   .+�5   
   � $��0��6   .+�5      �� $*��$单兵作战作训综合管理系统��6   $��Center=�6,   $��Center=�6G   $��20(�6d   5      � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $��15 �6#   �� 5      $��* �6#   �� 5      $��15 �6#   .+�5      � $��1��6    5      �� 5      $��* �6$   �� 5      $	��100 �6$   �� 5       $��25 �6$   .+�5"      � $��1��6   $	��5,0q�6$    5#      �� 5$      $��* �6+   �� 5%      $
��2.5* �6+    5'      �� 5(      $��* �6(   �� 5)      $��10 �6(   �� 5*      $��* �6(   .+�5,      �� $��用户名: ��6    $��Center=�6-   $��Right=�6H   $��16(�6d   �� 5-      $��
密  码: ��6    $��2��6.   $��Center=�6;   $��Right=�6V   $��16(�6r   �� 5.      $��1��68   $��25��6H   ��6   �� *
UserName+6H   G� 5/      A   ;SC_AnalysisSystem_Core.AttachProperty.PasswordBindingHelper   IsPasswordBindingEnabled . $��2��6�   $��1��6�   $��25��6�      BindedPassword 6]   �� *
Password+6�   50      � 51      $��2��6   .+�52      �� $��Center=�6;   $
��Left=�6V   $��5,0,0,0q�6q   ��6    �� *	LoginInfo+6q   �� 53      $��Center=�6   $
��Left=�68   $��新用户注册��6S   $��10(�6c   
����  �
������ $��5,0,0,0q�6�   �� 54      $��Center=�6   $��Right=�68   $��忘记密码？��6T   $��10(�6d   
����  �
������ $��0,0,5,0q�6�   55      56      57      �� 58   
    �� Orientation$ 
Horizontal=�6   $��2��6.   $��Center=�6;   $��Center=�6V   .+�59      �� $��登录��6   $��30��6"   $��80��6.   ��69   �� *LoginCommand+5:      5;      5<      