   M S B A M L   `   `   `  ����  DrawApp    DrawApp.InputDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  InputDialog��5   	    � SizeToContent$ WidthAndHeight=�6    � WindowStartupLocation$ CenterScreen=�6<   .��5      � $��15q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $��* �6   �� 5
      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��1��6   $��2��6    .��6-   	Top text:68   �� 5   
   -   5        $��InputTop��5      $��1��6"   $��2��62   6A   �� 5   
   $��0��6   $��3��6    .��6-   Bottom text:6;   �� 5   
   -   5        $��InputBottom��5      $��1��6%   $��3��65   6D   �� 5   
   $��0��6   $��4��6    .��6-   
Left text:69   �� 5   
   -   5        $��	InputLeft��5      $��1��6#   $��4��63   6B   �� 5   
   $��0��6   $��5��6    .��6-   Right text:6:   �� 5   
   -   5        $��
InputRight��5      $��1��6$   $��5��64   6C   � 5   
   $��6��6   $��2��6!   $��Right=�65   $��0,15,0,0q�6Q   .+�5       �� -   5        $��btnDialogOk��5    &    �� 	IsDefault . $��60��6S   $��0,0,10,0q�6a   .��6s   _Ok6x   �� 5!       �� IsCancel . $��60��6%   .��63   	_Cancel6<   5"      5#      5$      