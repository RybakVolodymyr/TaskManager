   M S B A M L   `   `   `  ����  TaskManager'    !TaskManager.Views.ProcessListView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      �   � RowDefinitions  5	   
   �� 5
      $��* �6   6)   �� 5      $
��Auto �6   6,    � ColumnDefinitions 5   
   �� 5      $��5 �6   6+   �� 5      $��* �6   6+   �� 5      $��5 �6   6+   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��ProcessesDataGrid��5   p   $��0��6      SelectionMode$ Single=�6    $��1��6`      AutoGenerateColumns .   �� SelectedItem 67   �� *SelectedProcess+��6�   �� *	Processes+ �� Mode$ OneWay=� � ContextMenu 5      �� 5      .@�5      m� $��	More Info��6    m� Command 62   �� *OpenMoreInfoCommand+m� 5      $��Open in file explorer��6    6>   �� *OpenFileFolderCommand+m� 5      $��Kill process��6    65   �� *KillCommand+5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Process Name��6%   
   MaxWidth$	
 200Q�6;      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6\   �� *Name+6w     5      $	 
Process ID��6%   $	
 155Q�69    .  6Z   �� *Id+6s     5      $	 	Is Active��6%   $	
 155Q�68    .  6Y   �� *
IsActive+6x     5      $	 %, CPU��6%   $	
 155Q�65    .  6V   �� *Cpu+6p     5       $	 MB, RAM��6%   $	
 155Q�66    .  6W   �� *Ram+6q     5!      $	 Threads Created��6%   $	
 155Q�6>    .  6_   �� *ThreadsNumber+6�     5"      $	 Username��6%   $	
 155Q�67    .  6X   �� *
Username+6w     5#      $	 	File Path��6%   $	
 550Q�68    .  6Y   �� *
FilePath+6x     5$      $	 Started��6%   $	
 155Q�66    .  6W   �� *StartedDateTime+6}   5&      �� 5'   
   $��1��6   $��1��6   $��EndTask��6.   $��2q�6@   {�6K   �� *IsItemSelected+$ OneWay=� �� UpdateSourceTrigger$ PropertyChanged=���6�   �� *KillCommand+6�   5(      5)      