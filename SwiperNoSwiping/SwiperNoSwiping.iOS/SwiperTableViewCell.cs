// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using CoreGraphics;

namespace SwiperNoSwiping.iOS
{

	public partial class SwiperTableViewCell : UITableViewCell
	{
		public static readonly string CellId = "SwiperTableViewCell";


		public bool LockRight { get; set; }

		public bool LeftAvailable { get; set; }


		public bool Open { 
			get {
				return LeftOpen || RightOpen;
			}
		}

		public bool LeftOpen {
			get {
				return ContentScrollView.ContentOffset != CGPoint.Empty && ContentScrollView.ContentOffset.X < 0f;
			}
		}

		public nfloat LeftButtonOffset {
			get {
				return LeftButton.Frame.Width;
			}
		}

		public bool RightOpen {
			get {
				return ContentScrollView.ContentOffset != CGPoint.Empty && ContentScrollView.ContentOffset.X > 0f;
			}
		}


		public nfloat RightButtonOffset {
			get {
				return Frame.Width - RightButton.Frame.Width;
			}
		}


		public SwiperTableViewCell (IntPtr handle) : base (handle)
		{
		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			ContentScrollView.Delegate = new SlidingCellScrollDelegate(this);

			ContentScrollView.AlwaysBounceVertical = false;
			ContentScrollView.DirectionalLockEnabled = true;
		}


		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			ContentScrollView.ContentInset = new UIEdgeInsets(0.0f, LeftAvailable ? LeftButton.Frame.Width : 0.0f, 0.0f, LockRight ? 0.0f : RightButton.Frame.Width);
			ContentScrollView.SetContentOffset (CGPoint.Empty, true);
			LeftButton.Hidden = !LeftAvailable;
		}


		public void SetData (string title)
		{
			SliderTitleLabel.Text = title;
		}


		public void CloseSlideButtons ()
		{
			ContentScrollView.SetContentOffset (CGPoint.Empty, true);
		}


		public class SlidingCellScrollDelegate : UIScrollViewDelegate
		{
			readonly SwiperTableViewCell cell;

			public SlidingCellScrollDelegate (SwiperTableViewCell cell)
			{
				this.cell = cell;
			}

			public override void Scrolled (UIScrollView scrollView)
			{
				if (scrollView.ContentOffset.X > 0.0f && cell.LockRight) {
					scrollView.ContentOffset = CGPoint.Empty;
				}

				if (scrollView.ContentOffset.X < 0 && !cell.LeftAvailable) {
					scrollView.ContentOffset = CGPoint.Empty;
				}
			}

			public override void WillEndDragging (UIScrollView scrollView, CGPoint velocity, ref CGPoint targetContentOffset)
			{
				if (scrollView.ContentOffset.X > cell.RightButton.Frame.Width - 10) {
					targetContentOffset.X = cell.RightButton.Frame.Width;
				} else if (scrollView.ContentOffset.X < -cell.LeftButton.Frame.Width + 10) {
					targetContentOffset.X = -cell.LeftButton.Frame.Width;
				} else {
					targetContentOffset = CGPoint.Empty;
				}
			}
		}
	}
}