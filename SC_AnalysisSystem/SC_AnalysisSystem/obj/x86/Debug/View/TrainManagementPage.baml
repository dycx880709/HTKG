   M S B A M L   `   `   `  ����L  HSC_AnalysisSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:SC_AnalysisSystem.Resources.Controls$SC_AnalysisSystem.Resources.Controls  5      I)clr-namespace:SC_AnalysisSystem.ViewModelSC_AnalysisSystem.ViewModel  5      0    *SC_AnalysisSystem.View.TrainManagementPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Gcontrols2clr-namespace:SC_AnalysisSystem.Resources.Controls     5      8vm)clr-namespace:SC_AnalysisSystem.ViewModel     5        N� 
Background
  ����� N� Title$ TrainManagementPage��5
       � 	Resources 5      :   4SC_AnalysisSystem.ViewModel.TrainManagementViewModel   TrainManagementViewModel��%   &	          5   
   6&   5       � DataContext 5      �� 5   
    �� Source# [  6   .��5      �  � ColumnDefinitions 5   
   �� 5      $��3* �6   �� 5      $��1 �6   �� 5      $	��200 �6    � RowDefinitions 5   
   �� 5      $��3* �6   �� 5      $��1 �6   �� 5      $	��150 �6   .+�5   
   � -   5         �Name$ mainTrainShow��5      .+�5      6   0SC_AnalysisSystem.Resources.Controls.AnimatedGIF  -   5          Name$	 gif��5   #   $��UniformToFill=�6w   	   	GIFSource	 60   �� *SelectedValue.GifSource+
 �� ElementName$
 sub_ListBox��6w   5      �� 5   
   
�������$��1��6#   $��3��63   �� 5    
   
�������$��1��6#   $��3��60   �� 5!   
   -   5         ��Name$ personInfos��5!      $��训练人员列表��6(   $��20(�68   $��2��6F   $��0��6S   $��5q�6c   .��5"      �� -   5         ��Name$ person_ListBox��5"      $	��5,0q�6R   ��6.   �� *PersonInfos+��5#      �� 5$       �� 
TargetType" ��6&   .R�5%      �� ._�5&      �  5'   "   �� 5(   &   $��* �67   �� 5)   &   $
��Auto �67   .+�5+   "   ��  K� IsItemsHost .  �� Orientation$ 
Horizontal=�6@   �� 5,   "   $��1��6)   $��添加��69   $��25(�6F   $��addUser��6v   $��90��5-   )   $��90��65   ��5,   T   �� *ButtonCommand+5-   5   5.      5/      50      ��52      �� 53       �� 
TargetType" ��6   .�54      ��  �� Property � �� �� Value$	 120��63   �� 55       � �� 56      �� 57   "   " ��62   .R�58   &   �� #��z��6-   #��z��6W   #��z��6y   ._�59   *   �� #��z��6;   #��z��6z   5:   '   5;   #   5=       �� Triggers 5>      5@      ��5B      �� 5C      .R�5D      �  5E      �� 5F   "   $	��120 �63   �� 5G   "   $	��100 �63   .+�5I      �� $��0��6$   $
��Fill=�6N   ��64   �� *Source+6N   �� 5J      $��1��6)   .+�5K   "   �� $ 
Horizontal=�6-   .+�5L   &   �� $��姓名: ��60   �� 5M   &   ��60   �� *
UserName+5N   #   �� 5O   "   $ 
Horizontal=�6-   $	��0,5q�6F   .+�5P   &   �� $��角色: ��60   �� 5Q   &   ��60   �� *Role+5R   #   �� 5S   "   $ 
Horizontal=�6-   .+�5T   &   �� $��评分: ��60   �� 5U   &   ��60   �� *Source+5V   #   5W      5X      5Y      5[      5\      � 5]   
   -   5        $ subInfos��5]      $��2��6!    5^      �� 5_      $��20 �6    �� 5`      $��* �6    .+�5b      �� -   5        $ sub_ListBox��5b      $��1��6+   ��68   �� *SubjectInfos+��5c      �� 5d      .R�5e      � $	��120��6    5f      �� 5g   "   $	��120 �63   �� 5h   "   $��* �63   .+�5j      �� $��0��6$   $��Uniform=�6S   ��64   �� *ImageSource+6S   �� 5k      $��1��6)   $
��5, 2q�69   .+�5l   "   �� $ 
Horizontal=�6-   .+�5m   &   �� $��	名称：��60   �� 5n   &   ��60   �� *SubjectName+5o   #   �� 5p   "   $ 
Horizontal=�6-   .+�5q   &   �� $��	难度：��60   �� 5r   &   ��60   �� *Score+5s   #   5t      5u      5v      5x      5y      �� 5z   
   $��2��6   $��2��6   $��10,10,10,20q�6.   $��Bottom=�6C   $��90��6^   $��创建��6�   $��25(�6�   ��6j   �� *ButtonCommand+6�   5{      5|      