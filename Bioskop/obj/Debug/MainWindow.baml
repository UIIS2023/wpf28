   M S B A M L   `   `   `  ����B  >Bioskop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:BioskopBioskop  5   	       Bioskop.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Bioskop     5   	     � Title$  Bioskop��5   	   $	��350��6   $
��auto��6&   .��5	      � .+�5
   
   �� $��Center=�6   $��30��62   $	��Top=�6?   $
��auto��6W    �� Orientation$ 
Horizontal=�6d   .+�5      �� -   5        $��btnFilm��5      $
��Film��6$   $��30��63   $��75��6?   $	��Top=�6J   $
��Left=�6b   6}   �� 5      -   5        $��btnKarta��5      $��Karta��6%   $��30��65   $��75��6A   $	��Top=�6L   $
��Left=�6d   6   �� 5      -   5        $��	btnRadnik��5      $��Radnik��6&   $��30��67   $��75��6C   $	��Top=�6N   $
��Left=�6f   6�   �� 5      -   5        $��btnKupac��5      $��Kupac��6%   $��30��65   $��75��6A   $	��Top=�6L   $
��Left=�6d   6   �� 5      -   5        $��btnTransakcija��5      $��Transakcija��6+   $��30��6A   $��75��6M   $	��Top=�6X   $
��Left=�6p   6�   �� 5      -   5        $��btnHranaPice��5      $��Hrana i pice��6)   $��30��6@   $��75��6L   $	��Top=�6W   $
��Left=�6o   6�   �� 5      -   5        $��btnSala��5      $
��Sala��6$   $��30��63   $��75��6?   $	��Top=�6J   $
��Left=�6b   6}   �� 5      -   5        $��btnZanr��5      $
��Zanr��6$   $��30��63   $��75��6?   $	��Top=�6J   $
��Left=�6b   6}   5      �� 5   
   $��Center=�6   $��30��62   $��Bottom=�6>   $
��auto��6Y   $ 
Horizontal=�6f   .+�5      �� -	   5        $��btnDodaj��5      $��Dodaj��6%   $��30��65   $��75��6A   6L   �� 5      -
   5        $��	btnIzmeni��5      $��Izmeni��6&   $��30��67   $��75��6C   6N   �� 5      -   5        $��	btnObrisi��5      $��Obrisi��6&   $��30��67   $��75��6C   6N   5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��dataGridCentralni��5      $��Center=�6,   $	��250��6I   $��Center=�6V   $
��auto��6q      
IsReadOnly .    AlternatingRowBackground
 ��� �   CanUserAddRows .     SelectionUnit$ FullRow=�5   \      SelectionMode$ Single=�6t   5      5      