   M S B A M L   `   `   `  ����J  FMarchingSquares, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MarchingSquaresMarchingSquares  5   	   E'clr-namespace:MarchingSquares.ViewModelMarchingSquares.ViewModel  5   	        MarchingSquares.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:MarchingSquares     5   	   =	viewModel'clr-namespace:MarchingSquares.ViewModel     5   	     � 
ResizeMode$  NoResize=�5	   	    � Title$ 
MainWindow��5
   	   $	��780��6   $
��1080��6)    � 	Resources 5      3   -MarchingSquares.ViewModel.MainWindowViewModel   vm��%   &	          5   
   6(   5      .��5      � -   5         �Name$ MainGrid��5   G   $	��780��6   $
��1080��6    � DataContext# [  6%    � ColumnDefinitions 5   
   �� 5      6!   .+�5   
   � .+�5      o� $��20��6   $	��Top=�6   .@�5      m� $��_Bitmap��6   .@�5      m� -   5        $��	_Generate��5      62   m� 5      $��_Open��6    m� Command 6.   �� *OpenFileDialogCommand+m� 5      $��_Save��6    6.   �� *OpenSaveFileDialogCommand+5      m� 5      $��_View��6   .@�5      	� $��	Grayscale��6"    d� 	IsChecked$
 TrueY�66   ��6G   �� *SwitchToGrayscaleCommand+	� 5      $��Color��6"   ��62   �� *SwitchToColorCommand+5      m� 5      -   5        $��_Marching Squares��5      6R   5      5      � 5    
   $��0��6   .+�5!      �� -   5         ��Name$ 
ZoomSlider��5!      	 � Minimum$
	 0.25Q�5"      
 � Maximum$
 4Q�5#       � Value$ 1Q�5$       �� TickFrequency$
 0.25Q�5%       �� IsSnapToTickEnabled$
 True��5&      $��
10, 24,0,0q�5'      5(      �� 5)      $
��1000��6   $	��700��6(   $��Visible=�65   $��Visible=�6]   .��5*      �� ��6   �� *VisibleImage+ � LayoutTransform 5+      �� 5,       �� ScaleX 6)   ��  �� ElementName$ 
ZoomSlider�� �� Path$ Value� �� ScaleY 5-   )   �� $ 
ZoomSlider��$ Value�5/      50      51      52      53      