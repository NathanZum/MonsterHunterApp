   M S B A M L   `   `   `  ����L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PresentationLayerPresentationLayer  5   	   #    PresentationLayer.MonsterView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:PresentationLayer     5   	   -   5          � Title$  Monster View��5   	   $	��600��6   $	��500��6+    � WindowStartupLocation$ CenterScreen=�67   .��5	      �  � RowDefinitions 5
   
   �� 5      $��10 �6   �� 5      $��45 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      �� 5      $��30 �6   �� 5      �� 5      �� 5      $��20 �6    � ColumnDefinitions 5   
   �� 5      $	��170 �6   �� 5      $��70 �6   �� 5      �� 5      $	��100 �6   .+�5   
   �� -   5         ��Name$ lblMonsterView��5      $��1��6(   $��View Monster��65   $
��Bold"�5       $��22(�6"   $��4��60   $��Center=�6D   �� 5!   
   -   5        $ lblMonsterName��5!      $��2��6(   $��Monster Name:��65   $
��10 2q�6M   �� 5"   
   -   5         ��Name$ txtMonsterName��5"      $��3��6*   $
��10 2q�67    �� 
IsReadOnly . 6E   �� 5#   
   -   5        $ lblMonsterType��5#      $��4��6(   $��Monster Type:��65   $
��10 2q�6M   �� 5$   
   -   5        $ txtMonsterType��5$      $��5��6*   $
��10 2q�67    . 6E   �� 5%   
   -   5        $ lblWeakness��5%      $��7��6%   $��Weaknesses:��62   $
��10 2q�6H   &   System.Windows.Controls.DataGrid  5&   
   -   5          Name$ datWeakness��5&      $��8��6(   $
��10 2q�66   $��2��6D      
IsReadOnly . 6X   �� 5'   
   -	   5        $ 
lblTerrain��5'      $��7��6$   $��2��61   $��Terrain:��6A   $
��10 2q�6T     5(   
   -
   5        $ 
datTerrain��5(      $��8��6'   $��2��64   $
��10 2q�6E    . 6S   �� 5)   
   -   5        $ lblMaterials��5)      $��9��6&   $��
Materials:��63   $
��10 2q�6H     5*   
   -   5        $ datMaterial��5*      $��10��6(   $
��10 2q�67   $��2��6E    . 6Y   �� 5+   
   -   5        $ lblPart��5+      $��9��6!   $��2��6.   $��Parts:��6>   $
��10 2q�6O     5,   
   -   5        $ datPart��5,      $��10��6$   $��2��62   $��
10 2 10 50q�6B   $��2��6V   $��2��6g    . 6{   �� 5-   
   -   5        	 ��Name$	 	btnMonAdd��5-      $��11��6$   $��10 30 60 30q�62   .��6_   Add Monster6l   �� 5.   
   -   5        $	 btnMonCancel��5.      $��11��6'   $��130 30 10 30q�65   $��2��6K   .��6z   Cancel6�   5/      50      