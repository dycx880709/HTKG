   M S B A M L   `   `   `  ����F  BAutoUpdater, Version=2.1.0.3, Culture=neutral, PublicKeyToken=null3clr-namespace:Ezhu.AutoUpdaterEzhu.AutoUpdater  5   	   (    "Ezhu.AutoUpdater.UI.DownCmpProcess   5      %myclr-namespace:Ezhu.AutoUpdater  5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5        !   Ezhu.AutoUpdater.WindowBase   Name$  w��5   1    � Title$ 软件更新提醒��6	   $	��350��6   $	��600��6%    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5	      $4 ./AutoUpdater;component/Theme/Custom/Style.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   6*   �� 5      $	��220 �6   6,   �� 5      $��* �6   6)   .+�5   
   � -   5         �Name$ borderTitle��5      $��0��6$   .+�5      �� $��3��6   ���#66c7ff �� CornerRadius$ 4,4,0,0��6>   �� 5       �� Orientation$ 
Horizontal=�6   $��1��62   $��Center=�6B   $��Center=�6]   .+�5      �� -   5        	 ��Name$	 title��5      $:��4发现新版本（2.1.0.1），是否现在更新？��6+   
�������$��14(�6\   5      �� 5      -   5        
 ��Name$
 CloseButton��5      $��2��6*   
������ $��0,0,2,2q�6S   $��0q�6d   
�������$��×��6�    � ToolTip$ 关闭��6�   $��Right=�6�   $��Center=�6�   $��25(�6�   $��35��6�   $��35��6�      DefaultButtonTemplate#��[  6   5      �� 5   
   $��1��6   $��
20,20,0,10q�6"   .+�5      �� $
��Left=�6   $	��Top=�63   $��新版本内容��6K   $��14(�6X   ���#6666666f   �� 5      -   5         ��Name$ listBox��5      $��0q�6'   $	��130��6;   
������   DefaultListBoxStyle#��[ 6   $��0,12,5,0q�6�   $��Hidden=�6�   ��6a   �� *Infos+��5      �� 5      .R�5      �� $ 
Horizontal=�6%   $��30��6>   .+�5       A� $��10��6&   $��10��61   ���#66c7ff$��Center=�6L   �� 5!      $��12,0,0,0q�6(   $��14(�6P   ���#999999$��Center=�6s   ��6:   �� *Info+6s   5"      5#      5%      5&      � 5'   
   $��2��6   .+�5(      �� $��1��6   $	��Top=�6    ���#eeeeee$��1q�6N   �� 5)      -   5         ��Name$ 	processGd��5)      $y�Hidden=�6,   .+�5*      �� -   5        $	 
txtProcess��5*      $��0%��60   $��Center=�6:   $��0,8,0,2q�6U   $��Center=�6f   ���#999999$��14(�6�   6�   �� 5+      -   5         ��Name$ bProcess��5+      ���#eeeeee$	��360��6@   $��12��6L   $��1q�6X   $��Center=�6l   $ 6��6�   ._�5,      �� -	   5        $ rectProcess��5,      ���#ade36d$��0��6G   $ 6��6Q   $
��Left=�6b   6   5-      5.      �� 5/      -
   5        $ startSp��5/      $ 
Horizontal=�6+   $��Center=�6D   .+�50      �� -   5        $
 NoButton��50      $��暂不更新��6+     MessageButtonStyle#��[ 6:   �� 51      -   5        $
 	YesButton��51      $��现在更新��6,    �� 	IsDefault . $��20,0,0,0q�6L   #��[ 6^   52      �� 53      -   5        $
 EndBtn��53      $��我知道了��6%   #��[ 64   $y�Hidden=�6`   54      55      56      