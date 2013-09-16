using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Syndication;

namespace DncShowcaseHub.DataModel
{

    class FeedItemIterator
    {
        private SyndicationFeed feed;
        private int index;

        public FeedItemIterator()
        {
            this.feed = null;
            this.index = 0;
        }

        public void AttachFeed(SyndicationFeed feed)
        {
            this.feed = feed;
            this.index = 0;
        }

        public bool MoveNext()
        {
            if (feed != null && index < feed.Items.Count - 1)
            {
                index++;
                return true;
            }
            return false;
        }

        public void MovePrevious()
        {
            if (feed != null && index > 0)
            {
                index--;
            }
        }

        public bool HasElements()
        {
            return feed != null && feed.Items.Count > 0;
        }

        public string GetTitle()
        {
            // Nothing to return yet.
            if (!HasElements())
            {
                return "(no title)";
            }

            if (feed.Items[index].Title != null)
            {
                return WebUtility.HtmlDecode(feed.Items[index].Title.Text);
            }

            return "(no title)";
        }

        public string GetContent()
        {
            // Nothing to return yet.
            if (!HasElements())
            {
                return "(no value)";
            }

            if ((feed.Items[index].Content != null) && (feed.Items[index].Content.Text != null))
            {
                return feed.Items[index].Content.Text;
            }
            else if (feed.Items[index].Summary != null && !string.IsNullOrEmpty(feed.Items[index].Summary.Text))
            {
                return feed.Items[index].Summary.Text;
            }

            return "(no value)";
        }

        public string GetIndexDescription()
        {
            // Nothing to return yet.
            if (!HasElements())
            {
                return "0 of 0";
            }

            return String.Format("{0} of {1}", index + 1, feed.Items.Count);
        }

        public Uri GetEditUri()
        {
            // Nothing to return yet.
            if (!HasElements())
            {
                return null;
            }

            return feed.Items[index].EditUri;
        }

        public SyndicationItem GetSyndicationItem()
        {
            // Nothing to return yet.
            if (!HasElements())
            {
                return null;
            }

            return feed.Items[index];
        }

        internal string GetAuthor()
        {
            if (feed.Items[index].Authors != null &&
                feed.Items[index].Authors.Count > 0)
            {
                string authors = "";
                foreach (var author in feed.Items[index].Authors)
                {
                    authors += (author.NodeValue + ",");
                }
                return authors.TrimEnd(',');
            }
            return "";
        }

        internal string GetUrlPath()
        {
            if (feed.Items[index].Links != null &&
                feed.Items[index].Links.Count > 0)
            {
                return feed.Items[index].Links[0].Uri.AbsoluteUri;
            }
            return "";
        }
    }

}
