   M S B A M L   `   `   `  ����M  IGestione Magazzino, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:Gestione_Magazzino.ConvertersGestione_Magazzino.Converters  5   	   #    Gestione_Magazzino.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   B
converters+clr-namespace:Gestione_Magazzino.Converters     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          � Title$  Gestione Magazzino��5   	   $	��800��5   	   $	��600��5	   	    � Icon$ Images/app.ico��5   	    � WindowStartupLocation$ CenterScreen=�5   	    +� FocusedElement 5   	   ��  �� ElementName$ TextBoxBarcode��.��5      � |�.   � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   o� $��0��6   $��2��5      ���	#FFFFFFFF|�.  .@�5      m� -   5        $��MenuItemOrders��5      $��Bottom=�5      |�.   m� Icon 5"      �� 5#      $��Images/orders.png��6   ��5%      �� 5&      $��Right=�6    $��Center=�5'       $��Ordini��5(       5*      m� 5+      |�.  $��Informazioni��6)   m� 5,      -   5        $��MenuItemLog��5,      |�.   5/      �� 50      $��Images/log.png��6   ��52      �� 53      $��Right=�6    $��Center=�54       $	��LOG��55       57      58      �� 59   
   -   5        $��ListViewWarehouse��59      $��1��5:      $��0��5;      |�.   � 	Resources 5>      ��%    	 �� View	 5?      �� 5@      .P�5A      �� $��Codice��6I   
 �� DisplayMemberBinding
 6%   �� *Id+6I   �� 5B      $
��Nome��6K   
 6%   �� *Name+6K   �� 5C      $��Prezzo (€)��5D   %   
 5C   %   �� *Price+ �� StringFormat$ 
{0:#,#.00}��5D   %   �� 5E      $��	Quantità��6O   
 6%   �� *
Quantity+6O   5F      ��5H      �� 5J       �� 
TargetType" ��6    �� 	Resources 5K      A   ;Gestione_Magazzino.Converters.WarehouseItemToColorConverter !  WarehouseItemToColorConverter��%   &	          5L      6C   5M      .�5N      ��  �� Property � �� �� Value 5O      ��  �� 	Converter# [  5P      5R      � 5S   
   $��1��6   $��1��5T      |�.  .+�5V      �  5W      �� 5X      $
��Auto �6$   �� 5Y      $��* �6$   �� 5Z      $
��Auto �6$   �� 5[      $
��Auto �6$    5]      �� 5^      $
��Auto �6'   �� 5_      .+�5a      �� -   5         ��Name$ TextBoxBarcode��5a      $��0��5b      $��2��5c      $��20(�5d      ���	#FF00AB15 �� 	MaxLength$ 50��5f       �� TextAlignment$ Right=�5g      .��5h   E   ��5i      �� 5j       �� Color$ Black��6*   .��5k   *   5l      �� 5m      -   5        $��DataGridShopping��5m      $��1��5n      $��0��5o      $��2��5p      |�.  	 5r      �� 5s      .P�5t      �� $��Codice��6Q   
 6-   �� *Id+6Q   �� 5u      $
��Nome��6S   
 6-   �� *Name+6S   �� 5v      $��Prezzo (€)��5w   -   
 5v   -   �� *Price+$ 
{0:#,#.00}��5w   -   �� 5x      $��	Quantità��6W   
 6-   �� *
Quantity+ �� CellTemplate 5y   "   �� 5z   &   .R�5{   *   ��  �� Orientation$ 
Horizontal=�65   .+�5|   .   �� $��-��65   �� 5}   .   �� 5~   .   $��+��65   5   +   5�   '   5�      �� 5�       5�   "   �� 5�   &   .R�5�   *   �� $��ButtonRemoveItem��61   -   5        $��Rimuovi��5�   1   5�   '   5�      5�      5�      �� 5�      $��2��6   $��0��5�      $��Totale (€):��5�      $��20(�5�      $
��Bold"�5�      �� 5�      -   5        $��TextBoxTotalPrice��5�      $��2��5�      $��1��5�      $��Center=�5�      |�.  $��20(�5�      $
��Bold"�5�       �� 
IsReadOnly . ��5�      ��  �� Path$ 
TotalPrice� �� RelativeSource ��  �� AncestorType" � �� Mode$ OneWay=� �� UpdateSourceTrigger$ PropertyChanged=��� 5�      -	   5         ��Name$ ButtonConfirmOrder��5�      $��3��5�      $��0��5�      $
��Left=�5�      ���	#FFFFFFFF|�.  $��18(�5�      ��5�      �� 5�      $ 
Horizontal=�6%   .+�5�      �� $��32��6$   $��32��5�   $   $��Images/ok.png��5�   $   �� 5�      $��CONFERMA��6$   5�      5�      �� 5�      -
   5        $��ButtonCancelOrder��5�      $��3��5�      $��1��5�      ���	#FFFFFFFF|�.  $��18(�5�      ��5�      �� 5�      $ 
Horizontal=�6%   .+�5�      �� $��32��6$   $��32��5�   $   $��Images/cancel.png��5�   $   �� 5�      $��CANCELLA��6$   5�      5�      5�      5�      �� 5�   
   $��2��6   $��0��5�      $ 
Horizontal=�5�      .+�5�      �� -   5        $ 	ButtonAdd��5�      ���	#FFFFFFFF|�.  $��20(�5�       � ToolTip$) #Aggiungi un elemento nel Magazzino.��5�      ��5�      �� 5�      $ 
Horizontal=�6!   .+�5�      �� $��32��6    $��32��5�       $��Images/add.png��5�       �� 5�      $��AGGIUNGI��6    5�      5�      �� 5�      -   5        $ ButtonModify��5�      ���	#FFFFFFFF|�.  $��20(�5�      $4 .Modifica l'elemento del Magazzino selezionato.��5�      ��5�      �� 5�      $ 
Horizontal=�6!   .+�5�      �� $��32��6    $��32��5�       $��Images/modify.png��5�       �� 5�      $��MODIFICA��6    5�      5�      �� 5�      -   5        $ ButtonDelete��5�      ���	#FFFFFFFF|�.  $��20(�5�      $~ xElimina gli elementi selezionati dal Magazzino. E' possibile tenere premuto CTRL e selezionare più elementi alla volta.��5�      ��5�      �� 5�      $ 
Horizontal=�6!   .+�5�      �� $��32��6    $��32��5�       $��Images/delete.png��5�       �� 5�      $��ELIMINA��6    5�      5�      5�      5�      5�      