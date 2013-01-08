///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.EmailNotification.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the Email Notification object
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
	public class EmailNotification
	{
        public enum EmailNotify
        { 
        
         EmailNotifyId, 
		 UserId, 
		 StoryNotify, 
		 NotesNotify, 
		 EventsNotify, 
		 GuestBookNotify, 
		 GiftsNotify, 
		 PhotoAlbumNotify, 
		 PhotosNotify, 
		 VideosNotify, 
		 CommentsNotify, 
		 MessagesNotify, 
		 NewsLetterNotify
        }

        


	/// <summary>
	/// Default Contructor
	/// <summary>	
        public EmailNotification()
	{}


	public int EmailNotifyId
	{ 
		get { return _EmailNotifyId; }
		set { _EmailNotifyId = value; }
	}
	private int _EmailNotifyId;


	public int UserId
	{ 
		get { return _UserId; }
		set { _UserId = value; }
	}
	private int _UserId;


	public bool StoryNotify
	{ 
		get { return _StoryNotify; }
		set { _StoryNotify = value; }
	}
	private bool _StoryNotify;


	public bool NotesNotify
	{ 
		get { return _NotesNotify; }
		set { _NotesNotify = value; }
	}
	private bool _NotesNotify;


	public bool EventsNotify
	{ 
		get { return _EventsNotify; }
		set { _EventsNotify = value; }
	}
	private bool _EventsNotify;


	public bool GuestBookNotify
	{ 
		get { return _GuestBookNotify; }
		set { _GuestBookNotify = value; }
	}
	private bool _GuestBookNotify;


	public bool GiftsNotify
	{ 
		get { return _GiftsNotify; }
		set { _GiftsNotify = value; }
	}
	private bool _GiftsNotify;


	public bool PhotoAlbumNotify
	{ 
		get { return _PhotoAlbumNotify; }
		set { _PhotoAlbumNotify = value; }
	}
	private bool _PhotoAlbumNotify;


	public bool PhotosNotify
	{ 
		get { return _PhotosNotify; }
		set { _PhotosNotify = value; }
	}
	private bool _PhotosNotify;


	public bool VideosNotify
	{ 
		get { return _VideosNotify; }
		set { _VideosNotify = value; }
	}
	private bool _VideosNotify;


	public bool CommentsNotify
	{ 
		get { return _CommentsNotify; }
		set { _CommentsNotify = value; }
	}
	private bool _CommentsNotify;


	public bool MessagesNotify
	{ 
		get { return _MessagesNotify; }
		set { _MessagesNotify = value; }
	}
	private bool _MessagesNotify;


	public bool NewsLetterNotify
	{ 
		get { return _NewsLetterNotify; }
		set { _NewsLetterNotify = value; }
	}
	private bool _NewsLetterNotify;

	/// <summary>
	/// User defined Contructor
	/// <summary>
	public EmailNotification(int EmailNotifyId, 
		int UserId, 
		bool StoryNotify, 
		bool NotesNotify, 
		bool EventsNotify, 
		bool GuestBookNotify, 
		bool GiftsNotify, 
		bool PhotoAlbumNotify, 
		bool PhotosNotify, 
		bool VideosNotify, 
		bool CommentsNotify, 
		bool MessagesNotify, 
		bool NewsLetterNotify)
	{
		this._EmailNotifyId = EmailNotifyId;
		this._UserId = UserId;
		this._StoryNotify = StoryNotify;
		this._NotesNotify = NotesNotify;
		this._EventsNotify = EventsNotify;
		this._GuestBookNotify = GuestBookNotify;
		this._GiftsNotify = GiftsNotify;
		this._PhotoAlbumNotify = PhotoAlbumNotify;
		this._PhotosNotify = PhotosNotify;
		this._VideosNotify = VideosNotify;
		this._CommentsNotify = CommentsNotify;
		this._MessagesNotify = MessagesNotify;
		this._NewsLetterNotify = NewsLetterNotify;
	}

	}
}
