   M S B A M L   `   `   `  ����  TaskManager     TaskManager.MoreInfoWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  MoreInfoWindow��5   	   $	��450��6    $	��800��6-   .��5      �  � ColumnDefinitions 5	   
   �� 5
      $��10 �6   6,   �� 5      $��* �6   6+   �� 5      $��10 �6   6,   �� 5      $
��Auto �6   6.   �� 5      $��10 �6   6,    � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)   .+�5   
   �� $��1��6   $��0��6    $��Modules��6-   $��Center=�6?   6^   &   System.Windows.Controls.DataGrid  5   
   $��1��6   $��1��6#      AutoGenerateColumns .  ��6L   �� *	Modules+ �� Mode$ OneWay=�   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6%      MaxWidth$	 300Q�63      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6T   �� *
ModuleName+6u     5      $ 	File Path��6%   $	 300Q�68    . 	 6Y   �� *
FileName+6x   5      �� 5   
   $��3��6   $��0��6    $��Threads��6-   $��Center=�6?   6^     5   
   $��3��6   $��1��6#    .  ��6L   �� *	Threads+$ OneWay=� 5        5      $ ID��6%   $	 300Q�61    . 	 6R   �� *Id+6k     5      $ State��6%   $	 300Q�64    . 	 6U   �� *ThreadState+6w     5       $ Started��6%   $	 300Q�66    . 	 6W   �� *	StartTime+6w   5"      5#      5$      