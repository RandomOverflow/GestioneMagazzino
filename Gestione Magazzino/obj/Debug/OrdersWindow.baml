   M S B A M L   `   `   `  ����  Gestione Magazzino%    Gestione_Magazzino.OrdersWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          � Title$  Ordini��5   	   $	��650��5   	   $	��450��5   	    � Icon$ Images/orders.png��5	   	    � WindowStartupLocation$ CenterOwner=�5   	   .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   .+�5   
   o� $��0��6   $��0��5      $��2��5      ���	#FFFFFFFF.@�5      m� -   5        $��MenuItemExport��5      $��Bottom=�5      |�.   m� Icon 5      �� 5       $��Images/export.png��6   ��5"      �� 5#      $��Right=�6    $��Center=�5$       $��Esporta��5%       5'      5(      �� 5)   
   -   5        $��ListViewOrders��5)      $��1��5*      $��0��5+       �� View 5.      �� 5/      .P�50      �� $��Codice��6I    �� DisplayMemberBinding 6%   �� *Id+6I   �� 52      $��Totale (€)��53   %    52   %   �� *
TotalPrice+ �� StringFormat$ 
{0:#,#.00}��53   %   �� 54      $��
Data & Ora��55   %    54   %   �� *
DateTime+$ dd-MM-yyyy HH:mm:ss��55   %   56      58      9� 5:   
   -   5        $��ExpanderMain��5:      $��1��5;      $��1��5<      	 9� ExpandDirection$	 Right=�5=      .��5>      �  5?      �� 5@      $
��Auto �6$   �� 5A      $��* �6$   .+�5C      �� $��0��6   $ ��OGGETTI ORDINE SELEZIONATO��5D      $��Medium"�5E      �� 5G      -   5        $��ListViewItems��5G      $��1��60    5H      �� 5I      .P�5J      �� $��Codice��6Q    6-   �� *Id+6Q   �� 5L      $
��Nome��6S    6-   �� *Name+6S   �� 5M      $��Prezzo (€)��5N   -    5M   -   �� *Price+$ 
{0:#,#.00}��5N   -   �� 5O      $��	Quantità��6W    6-   �� *
Quantity+6W   5P      5R      5S      5T      �� 5V   
   $��2��6   $��0��5W      $��2��5X      
 �� Orientation$
 
Horizontal=�5Y      .+�5Z      �� $��Center=�6   $��Anno:��66   �� 5[      -   5         ��Name$ ComboBoxYears��5[      $��Center=�5\       �� SelectedIndex$ 0��5]      5^      �� 5_      $��Center=�6   $��Mese:��66   �� 5`      -   5        $ ComboBoxMonths��5`      $��Center=�5a      $ 0��5b      5c      5d      5e      5f      