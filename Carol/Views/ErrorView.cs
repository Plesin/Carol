﻿using System;
using Foundation;
using AppKit;

namespace Carol.Views
{
    public partial class ErrorView : NSView
    {
        public static EventHandler RetryButtonClicked;
        #region Constructors

        // Called when created from unmanaged code
        public ErrorView(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public ErrorView(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
            ViewController.NetworkErrorOccurred += HandleNetworkError;
            ViewController.LyricsNotFoundOccurred += HandleLyricsNotFound;
			ViewController.NothingPlayingFound += HandleNothingPlayingFound;
			ViewController.NoMusicAppRunningFound += HandleNoMusicAppRunningFound;
			ViewController.MultiPlayingFound += HandleMultiPlayingFound;
        }

		#endregion

		partial void RetryButtonClick(NSObject sender)
		{
            RetryButtonClicked?.Invoke(this, null);
		}

        void HandleNetworkError(object sender, EventArgs e)
        {
            ErrorTextView.StringValue = "Looks like there is no internet connection.";
            RetryButton.Hidden = true;
        }

        void HandleLyricsNotFound(object sender, EventArgs e)
        {
            ErrorTextView.StringValue = "Could not find the lyrics of this song.";
        }

		void HandleNothingPlayingFound(object sender, EventArgs e)
		{
			ErrorTextView.StringValue = "No track is playing. Play something from your awesome collection.";
			IllustrationContainer.Image = new NSImage("illustration_no_music.png");
			RetryButton.Hidden = true;
		}

		void HandleNoMusicAppRunningFound(object sender, EventArgs e)
        {
			ErrorTextView.StringValue = "No music app is running. Play some music from one of the apps.";
			IllustrationContainer.Image = new NSImage("illustration_no_app.png");
			RetryButton.Hidden = true;
        }

		void HandleMultiPlayingFound(object sender, EventArgs e)
        {
			ErrorTextView.StringValue = "You playin' two songs at a time. Living in 3018.";
			IllustrationContainer.Image = new NSImage("illustration_two_songs.png");
			RetryButton.Hidden = true;
		}
	}
}
