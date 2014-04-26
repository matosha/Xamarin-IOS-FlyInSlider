// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace SlideOutPractice
{
	[Register ("SlideOutPracticeViewController")]
	partial class SlideOutPracticeViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton clickButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (clickButton != null) {
				clickButton.Dispose ();
				clickButton = null;
			}
		}
	}
}
