   M S B A M L   `   `   `  ����B  >ModuleA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ModuleA.ViewsModuleA.Views  5          ModuleA.Views.ViewA   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      P LPrism.Wpf, Version=6.3.0.0, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59$prismhttp://prismlibrary.com/  5      'localclr-namespace:ModuleA.Views  5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  . .��5      �  � ColumnDefinitions 5   
   �� 5      -   5         ��Name$ Column1��5      �� 5      -   5        $ Column2��5       � RowDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $��0��6   $��0��6   $��0,20,0,0q�6/   $
��Left=�6A   $��23��6\    �� TextWrapping$
 Wrap=�6h   $	��Top=�6�   $	��120��6�   ��6|   �� *Global.SelectedUnitCount+6�   �� 5   
   $��0��6   $��0��6   $��0,50,0,0q�6/   $
��Left=�6A   $��23��6\   $
 Wrap=�6h   $	��Top=�6�   $	��120��6�   ��6|   �� *	Unit.Hp+6�   �� 5   
   $��0��6   $��0��6   $��0,80,0,0q�6/   $
��Left=�6A   $��23��6\   $
 Wrap=�6h   $	��Top=�6�   $	��120��6�   ��6|   �� *	Message+6�   5      5      