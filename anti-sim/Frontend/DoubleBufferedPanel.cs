using System;
using System.Windows.Forms;

namespace AntSim
{
	//A class that inherits from C#'s Panel class. Used to enable double-buffer
	//(since the property is protected for some stupid reason)
	public class DoubleBufferedPanel : Panel
	{
		public DoubleBufferedPanel()
		{
			this.DoubleBuffered = true;
		}
	}
}

