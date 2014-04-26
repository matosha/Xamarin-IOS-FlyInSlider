using System;
using System.Drawing;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;

namespace SlideOutPractice
{
	public partial class SlideOutPracticeViewController : UIViewController
	{
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public SlideOutPracticeViewController ()
			: base (UserInterfaceIdiomIsPhone ? "SlideOutPracticeViewController_iPhone" : "SlideOutPracticeViewController_iPad", null)
		{
		}

		//FlyoutNavigationController navigation;
		public override void ViewDidLoad ()
		{

			//Create Navigation

			base.ViewDidLoad ();

			//Create The View you want to have sliding in
			UIView slidingContent = new UIView (new RectangleF(0,0,220,550));
			slidingContent.BackgroundColor = UIColor.Blue;

			//Add the view from the controller
			var lgSwipe = new LegendarySwipe (slidingContent);
			View.Add (lgSwipe.content);

			//have something that closes it
			var open = new UITapGestureRecognizer (() => lgSwipe.openIt());
			clickButton.UserInteractionEnabled = true;
			clickButton.AddGestureRecognizer (open);

			//have something that closes it 
			/*
			var close = new UITapGestureRecognizer (() => lgSwipe.closeIt());
			clickButton.UserInteractionEnabled = true;
			clickButton.AddGestureRecognizer (close);
			*/

		}
	}
	
		public class LegendarySwipe
		{
			public UIView content;
			public LegendarySwipe(UIView theViewToSlide)
			{
				content = theViewToSlide;
				content.Frame = new RectangleF(0-theViewToSlide.Frame.Width,theViewToSlide.Frame.Y,theViewToSlide.Frame.Width,theViewToSlide.Frame.Height);
				createGrab();
			}
			private void createGrab()
			{
				float dx = 0;
				var panGesture = new UIPanGestureRecognizer ((pg) => {
					if ((pg.State == UIGestureRecognizerState.Began || pg.State == UIGestureRecognizerState.Changed) && (pg.NumberOfTouches == 1)) {

						var p0 = pg.LocationInView (content);

						if (dx == 0)
							dx = p0.X - content.Center.X;

						var p1 = new PointF (p0.X - dx, content.Center.Y);
					//Dont let it go to far right
						if (p1.X <= content.Center.X)
							content.Center = p1;
					//Auto withdrawl after 33%
						if (p1.X < content.Frame.Width/3) {
						closeIt();
						}
					} else if (pg.State == UIGestureRecognizerState.Ended) {
						dx = 0;
					}
				});
				content.AddGestureRecognizer (panGesture);

			}
	
			public void openIt()
			{

				UIView.BeginAnimations ("slideAnimation");
				UIView.SetAnimationDuration (0.5);
				UIView.SetAnimationCurve (UIViewAnimationCurve.EaseInOut);
				UIView.SetAnimationRepeatCount (0);
				UIView.SetAnimationRepeatAutoreverses (false);
				UIView.SetAnimationDelegate (content);
				UIView.SetAnimationDidStopSelector (
					new Selector ("slideAnimationFinished:"));
				content.Center = new PointF (content.Frame.Width -content.Frame.Width / 2, content.Center.Y);

				UIView.CommitAnimations ();
				Console.WriteLine ("Opened Slider");
			}

			public void closeIt()
			{
				UIView.BeginAnimations ("slideAnimation");
				UIView.SetAnimationDuration (0.5);
				UIView.SetAnimationCurve (UIViewAnimationCurve.EaseInOut);
				UIView.SetAnimationRepeatCount (0);
				UIView.SetAnimationRepeatAutoreverses (false);
				UIView.SetAnimationDelegate (content);
				UIView.SetAnimationDidStopSelector (
					new Selector ("slideAnimationFinished:"));
				content.Center = new PointF (0 -content.Frame.Width / 2, content.Center.Y);

				UIView.CommitAnimations ();
				Console.WriteLine ("Closed Slider");

			}
		}

}

