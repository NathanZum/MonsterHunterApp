   M S B A M L   `   `   `  ����L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PresentationLayerPresentationLayer  5   	   #    PresentationLayer.TerrainView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:PresentationLayer     5   	   -   5          � Title$  TerrainView��5   	   $	��450��6   $	��500��6*    � WindowStartupLocation$ CenterScreen=�66   .��5	      �  � RowDefinitions 5
   
   �� 5      $��10 �6   �� 5      $��45 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��50 �6    � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� -   5         ��Name$ lblTerrainView��5      $��2��6(   $��1��6<   $��Add Terrain��6I   $
��Bold"�5      $��22(�6"   $��Center=�60   �� 5   
   -   5        $ lblTerrainList��5      $��3��6(   $��List of Terrains:��65   $��2��6Q   $��Center=�6e   �� 5   
   -   5        $ 
lblTerrain��5      $��Terrain Name : ��6$   $��5��6>   $��Right=�6K   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ datTerrains��5      $��4��6(   $��2��65   $��150 5q�6I      
IsReadOnly . 6X   �� 5   
   -   5         ��Name$ txtTerrainName��5      $��1��6*   $��5��6:   $��8 2 40 2q�6G   �� 5    
   -   5        $ lblMonsterName��5       $��Monster Name : ��6(   $��6��6B   $��Right=�6O   �� 5!   
   -   5        $ txtMonsterName��5!      $��1��6*   $��6��6:   $��8 2 40 2q�6G    �� 
IsReadOnly . 6Y   �� 5"   
   -	   5        $ lblTerrainSelect��5"      $��Terrain to Assign : ��6*   $��7��6I   $��Right=�6V   �� 5#   
   -
   5        	 ��Name$	 cmbTerrains��5#      $��7��6(   $��1��65   $
��10 0q�6E   $	��200��6S   $��20��6_   $
��Left=�6k   ��5$      �� 5%      .R�5&      �� 
 �� Orientation$
 
Horizontal=�6!   .+�5'      �� ��6$   �� 5(      5)      5+      �� 5,   
   -   5         ��Name$ btnAddTerrain��5,      $��9��6(   $��
150 5 10 5q�65   $	��Add��6I   6W   �� 5-   
   -   5        $ btnCancelTerrain��5-      $��9��6+   $��1��68   $��	8 5 150 5q�6H   $��Cancel��6[   6l   5.      5/      