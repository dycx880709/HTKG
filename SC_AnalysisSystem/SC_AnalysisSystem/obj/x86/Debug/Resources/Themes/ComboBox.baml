   M S B A M L   `   `   `  ����Q  MSC_AnalysisSystem_Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}Sclr-namespace:SC_AnalysisSystem_Core.AttachProperty;assembly=SC_AnalysisSystem_Core%SC_AnalysisSystem_Core.AttachProperty  5      sNclr-namespace:SC_AnalysisSystem_Core.Converter;assembly=SC_AnalysisSystem_Core SC_AnalysisSystem_Core.Converter  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� 
TargetType   LabelComboBox  DefaultComboBox �� BasedOn �� Property �� ValueA    ;SC_AnalysisSystem_Core.AttachProperty.ControlAttachProperty �� 
TargetType  TextLabelBackground  ComboBoxItemStyle ��Name J� IsHitTestVisible J� SnapsToDevicePixels ��Name	 �� Triggers
 P� Property P� Value �� 
TargetName  ItemSelectedBackground  ItemMouseOverBackground  TextForeground  TextBackground  ControlBorderBrush 	 FocusBackground 
 FocusBorderBrush  MouseOverBorderBrush  
FontFamily  FontSize �Name    CornerRadius �� CornerRadius � ColumnDefinitions ��Name    LabelTemplate    Label K� ZIndex    AttachContent d�Name d� 	IsChecked �� Mode �� RelativeSource �� Path1   +SC_AnalysisSystem_Core.Converter.XConverter   TrueToFalseConverter �� 	Converter ��Name �� Padding    	Watermark  WatermarkOpacity  J� Opacity! �� TextAlignment" �� SelectionBoxItemTemplate# �� SelectionBoxItem#$ �� SelectionBoxItemStringFormat% �� ContentStringFormat& ��Name  EditableTextBoxStyle' �� 
IsReadOnly( �� 
IsReadOnly) �� Text* �Name+ � AllowsTransparency  PopupBackground, ��Name- ��Name-  'System.Windows.Input.KeyboardNavigation.   DirectionalNavigation/ �� Value0 �� Binding  DisableOpacity�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      ^attachSclr-namespace:SC_AnalysisSystem_Core.AttachProperty;assembly=SC_AnalysisSystem_Core  5      \	converterNclr-namespace:SC_AnalysisSystem_Core.Converter;assembly=SC_AnalysisSystem_Core  5      %�  &	        7  7  &	 8    7  7  &	 U    7  7  7  7 	 7 
 7  7  7  7  7  7  7  7  �� 5      "  ��6   8   .�5   
   ��  � ��$	 260��6"   60   �� 5   
    �@  LabelTemplate 5	      �� 5
      " ��6"   .R�5      �� $��60��6   8�� ._�5      �� $��Center=�6$   $��Right=�6?   $��2q�6[   
���   �#��z��6y   6�   5      5      5      5      �� 5      "  ��6   .�5   
   ��  � ��$ Stretch=�67   �� 5   
    � ��$ Center=�65   �� 5   
    �@��ClearTypeHint$ Enabled=�68   �� 5   
    � ��$ 0q�6,   �� 5   
    � ��$ 28��6#   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      � #��z��6   $��0,0.5q�6E   .+�5      �� $ ItemBackground��6!    .  # z 6R   #��z��5   !   #��z��6U   #��z��5    !   �� 5!      $ contentPresenter��6+   #��z��6E   #��z��6h   #��z��5"   +   5#      	 5$      P� 5%      
 �@��
IsSelected . . �5&      �� $ ItemBackground��6%   8    � ��6W   5'      P� 5(      
 �@J�IsMouseOver . . �5)      �� $ ItemBackground��6%   8   � ��6W   5*      5,      5.      5/      �� 58      "  ��6   .�59   
   ��  � ��$ 30��6#   �� 5:   
    � ��$	 200��6"   �� 5;   
    � ��8   6'   �� 5<   
    � ��8  6'   �� 5=   
    � ��$ Stretch=�67   �� 5>   
    � ��$ Center=�65   �� 5?   
    � ��$ 0q�6,   �� 5@   
    � ��8  6(   �� 5A   
    �@  FocusBackground8  6I   �� 5B   
    �@  FocusBorderBrush8  6J   �� 5C   
    �@  MouseOverBorderBrush8  6N   �� 5D   
    � ��8  6'   �� 5E   
    � ��8  6%   �� 5F   
    � ��8  6/   �� 5G   
    �@J�SnapsToDevicePixels . 60   �� 5H   
    �@��MaxDropDownHeight$	 400��6.   �� 5I   
    � �� .  6:   �� 5J   
    � �� 5K      �� 5L      " ��6"   .R�5M      � $ 	PART_Root��6   .+�5N      �� $ Bg��6!   # z 6-   # z 5O   !   #��z��5P   !   #��z��6M   #��z��6�   � 5Q       5R      �� 5S   "   $
��Auto �63   �� 5T   "   $��* �63   �� 5U   "   $
��Auto �63   �� 5V   "   $��21 �63   .+�5Y      �� $ Label��6-   #��z 6<   ��.   .  #��z 5Z   -   $��1,1,0,1q�6l   �� 5\      $ PART_AttachContent��6%   $ 2��6A   $��2��6R   $��Center=�6b   $��Center=�6}   ._�5]   "   �� $��Center=�61   $��Center=�6L   #��z 6n   5^      d� 5`      $ PART_DropDownToggle��6+   ��.  $��2 1 2 1q�6�   $��1��5a   *   $��3��6;   
������� 5`   Z   �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+{�5a   O   �� $ 
IsReadOnly� �� *TemplatedParent+# Z $ OneWay=�5b   w   �� 5d      $��1��6%   ._�5e   "   �� $ Message��6,   $ 0q�6=   $y�	Collapsed=�6I   #��z 6`   #��z��5f   (    .  8  	 $
��Left=�6�   $! Center=�6�   $��Center=�5g   (   $��5,2,5,2q�6C   5h      � 5j      $ ContentSite��63   $��1��6#   $��2 0 0 0q�6H   .+�5k   "   �� $ PART_SelectedItem��63   #��z" 6N   #��z��5l   3   #��z# 5m   3   #% z$ 6`   $��Stretch=�5n   3   $��2,0,2,0q�6Q    .  # z 5o   L   #��z��5p   3   �� 5r   "   $& PART_EditableTextBox��6+   8��
 $y�	Collapsed=�6w   $��Stretch=�5s   *   # z 6H    . #��z��5t   *   #��z��6t   #( z' 5u   *   #��z��6T   #��z��6~   #��z) 5v   *   #��z��6H   5w      5x      � 5z      $* 
PART_Popup��6    + . |�.  #��� .�5|       $��Bottom=�6y   ��5{       �� *IsDropDownOpen+ �� *TemplatedParent+.��5}      � ��6#   �� *ActualWidth+ �� *TemplatedParent+��5~   #   �� *MaxDropDownHeight+ �� *TemplatedParent+.+�5   "   �� $ PopupBorder��6)   #��z��6>   $��Stretch=�6r   $
��Auto��5�   )   #��z��67   8�� 5�   #   �� 5�   "   $, DropDownScrollViewer��6/   $��0q�6M   $��1q�6a   .��5�   &   �� $- ItemsPresenter��65   $. 	Contained=�6M   5�   #   5�      5�      5�      	 5�      �� 5�      $/  ��6j   0 6&   ��  �� *Self+$
 Text�.a�5�      �� $ Message��6%    � y�$ Visible=�6P   5�      P� 5�      
 �@��
IsEditable . . �5�      �� $ PART_EditableTextBox��6%    � y�$ Visible=�6]   �� 5�      $ PART_SelectedItem��6%    � y�$ 	Collapsed=�6Z   �� 5�      $ PART_DropDownToggle��6%    � ��$ 3��6]   �� 5�      $ PART_DropDownToggle��6%    � ��$ 1��6a   �� 5�      $ PART_DropDownToggle��6%    � ��
 ���� 6\   �� 5�       � �� .  6:   �� 5�      $ PART_DropDownToggle��6%    � |� .  6[   5�      P� 5�      
 �@J�IsMouseOver . . �5�      ��  6<   �� $9 3(attach:ControlAttachProperty.MouseOverBorderBrush)� �� *Self+ � ��5�      P� 5�      
 �@J�	IsFocused . . �5�      ��  6=   �� $5 /(attach:ControlAttachProperty.FocusBorderBrush)� �� *Self+ � ��5�      P� 5�      
 �@J�IsKeyboardFocusWithin . . �5�      ��  6=   �� $5 /(attach:ControlAttachProperty.FocusBorderBrush)� �� *Self+ � ��5�      P� 5�      
 � {� .  . �5�      �� $ 	PART_Root��6%   8   �@J�Opacity6O   5�      5�      5�      5�      5�      