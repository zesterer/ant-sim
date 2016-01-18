using System;
using System.Windows.Forms;

namespace AntSim
{
	public class DoubleBufferedPanel : Panel
	{
		public DoubleBufferedPanel()
		{
			this.DoubleBuffered = true;
		}
	}
}

