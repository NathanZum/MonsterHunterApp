   M S B A M L   `   `   `  ����L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PresentationLayerPresentationLayer  5   	   )    #PresentationLayer.frmUpdatePassword   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:PresentationLayer     5   	   -   5          � Title$  frmUpdatePassword��5   	   $	��300��6#   $	��440��60    � WindowStartupLocation$ CenterScreen=�6S   .��5	      �  � RowDefinitions 5
   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      $	��160 �6   �� 5      .+�5   
   �� -   5         ��Name$ txtInstructions��5      $��0��6-   $��2��5      $
��Wrap=�6(    �� TextAlignment$ Center=�5      $
��50 5q�6+   $
��Bold"�5      $��Yu Gothic UI)�6&   A?This text is in a column span. It stretches across the columns.5      �� 5   
   -   5         ��Name$ lblUsername��5      $��1��6%   $��	10 5 20 5q�62   $��Center=�5      $��Right=�5      .��63   	Username:6>   �� 5    
   -   5        $ lblOldPassword��5       $��2��6(   $��	10 5 20 5q�65   $��Center=�5!      $��Right=�5"      .��63   Old Password:6B   �� 5#   
   -   5        $ lblNewPassword��5#      $��3��6(   $��	10 5 20 5q�65   $��Center=�5$      $��Right=�5%      .��63   New Password:6B   �� 5&   
   -   5        $ lblConfirmPassword��5&      $��4��6,   $��	10 5 20 5q�69   $��Center=�5'      $��Right=�5(      .��63   Confirm Password:6F   �� 5)   
   -   5         ��Name$ txtUsername��5)      $��1��6'   $��1��64   $��	10 5 20 5q�6D   $��Center=�5*      $��Center=�64   G� 5+   
   -   5         G�Name$ txtOldPassword��5+      $��2��6.   $��1��6;   $��	10 5 20 5q�6K   $��Center=�5,      $��Center=�64   G� 5-   
   -	   5        $ txtNewPassword��5-      $��3��6.   $��1��6;   $��	10 5 20 5q�6K   $��Center=�5.      $��Center=�64   G� 5/   
   -
   5        $ txtConfirmPassword��5/      $��4��62   $��1��6?   $��	10 5 20 5q�6O   $��Center=�50      $��Center=�64   �� 51   
   -   5        	 ��Name$	 	btnSubmit��51      $��5��6$   $��
10 5 10 10q�61   $��Submit��52      $��Right=�6"   $	��100��6>   6J   �� 53   
   -   5        $	 	btnCancel��53      $��5��6$   $��1��61   $��
10 5 10 10q�6A   $��Cancel��54      $
��Left=�6"   $	��100��6=   6I   55      56      