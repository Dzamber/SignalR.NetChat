   M S B A M L   `   `   `  ����L  HClientSygnalR.Net, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ClientSygnalR.NetClientSygnalR.Net  5   	   "    ClientSygnalR.Net.MainWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:ClientSygnalR.Net     5   	   -   5          � Title$  
MainWindow��5	   	   $	��470��6   $	��920��6)   .��5
      � $��	0,0,-70,0q�6   $	��440��6   $	��Top=�6+    � ColumnDefinitions 5   
   �� 5      $
��196* �6   �� 5      $	��13* �6   �� 5      $	��13* �6   �� 5      $
��108* �6   .+�5   
   �� -   5         ��Name$ messagesList��5      $��
145,38,0,0q�6(   $	��271��6<   $	��Top=�6I    J� RenderTransformOrigin$ 0.5,0.5&�6a   $
��Left=�6�   $	��635��6�   $��4��6�   z�5      V� 5      .~�5      �� �� 5      �� 5      T� 5      5      5      �� 5   
   -   5         ��Name$ connectButton��5      $��Polacz��6(   $
��Left=�69   $��
10,384,0,0q�6T   $	��Top=�6h   $ 19.308,0.202&�6�   �� 5   
   -   5        $ 
sendButton��5      $��Wyslij Wiadomosc��6%   $
��Left=�6@   $��
14,364,0,0q�6[   $	��Top=�6o   $��3��6�   $ -0.791,1.154&�6�   �� 5   
   -   5         ��Name$ nickname��5      $
��Left=�6$   $��
10,354,0,0q�6?   $��-----------��6S    �� TextWrapping$
 Wrap=�6f   $	��Top=�6z   $	��115��6�   $��25��6�   ${�False��6�   �� 5   
   -   5        $ 
messagebox��5      $
��Left=�6&   $��145,329,0,0q�6A   $�� ��6V   $
 Wrap=�6^   $	��Top=�6r   $	��635��6�   $��25��6�   $ 0.5,0.5&�6�   $��4��6�   .��6�   5       �� 5!   
   -   5        $ 	usersList��5!      $��10,38,0,111q�6%   $
��Left=�6:   $	��115��6U   6a   �� 5"   
   -   5        $ createNewRoom��5"      $��Stworz nowy pokoj��6(   $
��Left=�6D   $��134,339,0,0q�6_   $	��Top=�6t   $��3��6�   �� 5#   
   -	   5        $ channelRoomList��5#      $��134,38,0,111q�6+   $
��Left=�6A   $	��115��6\   $��3��6h    �� SelectionMode$ Single=�6�   �� 5$   
   -
   5        $ chatroomName��5$      $
��Left=�6(   $��134,366,0,0q�6C   $��roomName��6X   $
 Wrap=�6h   $	��Top=�6|   $	��115��6�   $��25��6�   $��3��6�   �� 5%   
   -   5         ��Name$ checkBox��5%      $��Wyslij Prywatna Wiadomosc��6%   $��20,384,196,0q�6I   $	��Top=�6_   $��18��6w   $��3��6�   $��1��6�   �� 5&   
   -   5        	 ��Name$	 label��5&      $��
Wyslij do:��6   $��1��64   $��35,393,300,0q�6D   $	��Top=�6Z   $ -1.474,-0.114&�6r   $��3��6�   $��36��6�   �� 5'   
   -   5        $	 target��5'      $
��----��6    $��3��6/   $
��Left=�6?   $��
12,393,0,0q�6Z   $	��Top=�6n   5)      5*      