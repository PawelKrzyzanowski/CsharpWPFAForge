   M S B A M L   `   `   `  ����G  CWPFVidRecApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:WPFVidRecAppWPFVidRecApp  5   	       WPFVidRecApp.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:WPFVidRecApp     5   	     � Title$  My Video Recorder��5   	   $	��619��6#   $	��536��60    � DataContext 5	        WPFVidRecApp.MyViewModel  5
   
   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �  5      �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      �� $��Video player��6   $��0,-4,0.4,3.6q�6+   �� 5      $��1��6   
���   �$��1q�5      ._�5      �� ��6   �� *Image+5      5       �� 5#   
   $��1��6   $��Source selection��6   � 5$   
   $��2��6    � IsSharedSizeScope .  5%      �� 5&      $��30 �6    �� 5'      $��30 �6    �� 5(      $��30 �6    �� 5)      $
��Auto �6     � ColumnDefinitions 5+      �� 5,      $
��Auto �6#   �� 5-      $��* �6#   .+�50      	� $��0,5,0,0q�6   $��Center=�51      $��Desktop��52       d� 	IsChecked 53      �� *IsDesktopSource+	� 54      $��1��6   $��Center=�55      $��Webcam��56       57      �� *IsWebcamSource+�� 58      $��1��6   $��1��59      $	��100��5:      $
��Left=�5;      $��Center=�5<       �� DisplayMemberPath$
 Name��5=      ��5>      �� *VideoDevices+ �� SelectedItem 5?      �� *CurrentDevice+	� 5@      $��2��6   $��0,0,5,0q�5A      $��Center=�5B      $��	Ip camera��5C       5D      �� *IsIpCameraSource+�� 5E      $��2��6   $��1��5F      $��Center=�5G      ��5H      �� *IpCameraUrl+�� 5I      $��3��6   $��2��5J      $��Center=�5K       �� Orientation$ 
Horizontal=�5L      .+�5M      �� $��0��6   $	��100��5N      $��30��5O      $��5q�5P      $��Right=�5Q      $��Start video��5S      ��5R      �� *StartSourceCommand+5S      �� 5T      $��1��6   $	��100��5U      $��30��5V      $��5q�5W      $
��Left=�5X      $��
Stop video��5Z      ��5Y      �� *StopSourceCommand+5Z      5[      5\      �� 5_   
   $��3��6   $��Video Commands��6   � 5a   
   $��4��6    5b      �� 5c      $��* �6#   �� 5d      $	��110 �6#   �� 5e      $	��110 �6#   �� 5f      $	��110 �6#   �� 5g      $��* �6#   .+�5i      �� $��1��6   $	��100��5j      $��30��5k      $��5q�5l      $��Right=�5m      $��	Start rec��5o      ��5n      �� *StartRecordingCommand+5o      �� 5p      $��2��6   $	��100��5q      $��5q�5r      $
��Left=�5s      $��Stop rec��5u      ��5t      �� *StopRecordingCommand+5u      �� 5v      $��3��6   $	��100��5w      $��5q�5x      $
��Left=�5y      $��Save snapshot��5{      ��5z      �� *SaveSnapshotCommand+5{      5|      5}      5~      