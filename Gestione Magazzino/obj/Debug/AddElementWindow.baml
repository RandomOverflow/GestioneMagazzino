   M S B A M L   `   `   `  ����  Gestione Magazzino)    #Gestione_Magazzino.AddElementWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Aggiungi Elemento��5   	   $��333.094��5   	   $��213.36��5   	    � Icon$ Images/add.png��5	   	    � 
ResizeMode$ NoResize=�5
   	    � WindowStartupLocation$ CenterOwner=�5   	   .��5      � $��10q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Codice: ��6   �� 5   
   -   5        $��TextBoxCode��5      $��0��5      $��1��5      �� 5   
   $��1��6   $��0��5      $��Nome: ��5       �� 5!   
   -   5        $��TextBoxName��5!      $��1��5"      $��1��5#      �� 5%   
   $��2��6   $��0��5&      $��Prezzo (€): ��5'      �� 5(   
   -   5        $��TextBoxPrice��5(      $��2��5)      $��1��5*      �� 5,   
   $��3��6   $��0��5-      $��Quantita': ��5.      �� 5/   
   -   5        $��TextBoxQuantity��5/      $��3��50      $��1��51      �� 52   
   $��4��6   $��0��53      $��2��54      $��Center=�55       �� Orientation$ 
Horizontal=�56      .+�57      �� -   5         ��Name$ ButtonConfirm��57      $��5,5,5,5q�58      $��18(�5:      ��5<      �� 5=      $ 
Horizontal=�6!   .+�5>      �� $��32��6    $��32��5?       $��Images/ok.png��5@       �� 5A      $��CONFERMA��6    5B      5D      �� 5E      �� 5F      -   5        $ ButtonCancel��5F      $��5,5,5,5q�5G      $��18(�5I      ��5J      �� 5K      $ 
Horizontal=�6!   .+�5L      �� $��32��6    $��32��5M       $��Images/cancel.png��5N       �� 5O      $��ANNULLA��6    5P      5R      5S      5T      5U      