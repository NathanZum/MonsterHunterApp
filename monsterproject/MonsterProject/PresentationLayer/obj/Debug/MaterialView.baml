   M S B A M L   `   `   `  ����L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PresentationLayerPresentationLayer  5   	   $    PresentationLayer.MaterialView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:PresentationLayer     5   	   -   5          �Name$  Material��5   	    � Title$ Material View��5   	   $	��450��6   $	��500��6,    � WindowStartupLocation$ CenterScreen=�68   .��5	      �  � RowDefinitions 5
   
   �� 5      $��10 �6   �� 5      $��45 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      �� 5      $��50 �6    � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� -   5         ��Name$ lblMaterialView��5      $��1��6)   $��View Material��66   $
��Bold"�5      $��22(�6"   $��4��60   $��Center=�6D   �� 5   
   -   5        $ lblMaterialName��5      $��2��6)   $��Material Name:��66   $
��10 2q�6O   �� 5   
   -   5         ��Name$ txtMaterialName��5      $��3��6+   $
��10 2q�68    �� 
IsReadOnly . 6F   �� 5   
   -   5        $ lblAddMatMonsterName��5      $��2��6.   $��1��6;   $��Monster Name:��6K   $
��10 2q�6c   �� 5    
   -   5         ��Name$ cmbMatMonsterName��5       $��3��6.   $��1��6;   $
��10 0q�6K   $	��200��6Y   $��20��6e   $
��Left=�6q   ��5!      �� 5"      .R�5#      �� 	 �� Orientation$	 
Horizontal=�6!   .+�5$      �� ��6$   �� 5%      5&      5(      �� 5)   
   -   5        $ lblViewMatMonsterName��5)      $��4��6/   $��Monster Name:��6<   $
��10 2q�6T   �� 5*   
   -   5        $ txtViewMatMonsterName��5*      $��5��61   $
��10 2q�6>    . 6L   �� 5+   
   -	   5        $ lblPrice��5+      $��6��6"   $��Price:��6/   $
��10 2q�6@   �� 5,   
   -
   5        $ txtPrice��5,      $��7��6$   $
��10 2q�61    . 6?   �� 5-   
   -   5        $ lblRates��5-      $��7��6"   $��1��6/   $"��(For drop rates 1.00 = 100%)��6?   $
��10 2q�6f   $��Italic$�6t   $��10(�6�   �� 5.   
   -   5        $ lblDropRates��5.      $��8��6&   $��Drop Rates:��63   $
��10 2q�6I   &   System.Windows.Controls.DataGrid  5/   
   -   5        
  Name$
 datDropRates��5/         
IsReadOnly . $��9��6;   $��10 0 8 0q�6H   50      �� 51   
   -   5        $ lblPartDropRates��51      $��8��6*   $��1��67   $��Broken Part Drop Rates:��6G   $
��10 2q�6i     52   
   -   5        $
 datPartDropRates��52       . $��9��6?   $��1��6L   $��6 0 10 0q�6\   53      �� 54   
   -   5         ��Name$ btnAddMaterial��54      $��10��6)   $��
150 5 10 5q�67   $	��Add��6K   6Y   �� 55   
   -   5        $ btnCancelMaterial��55      $��10��6,   $��1��6:   $��	8 5 150 5q�6J   $��Cancel��6]   6n   56      57      