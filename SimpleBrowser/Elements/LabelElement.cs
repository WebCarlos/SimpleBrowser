﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SimpleBrowser.Elements
{
	internal class LabelElement : HtmlElement
	{
		public LabelElement(XElement element)
			: base(element)
		{
		}
		public async override Task<ClickResult> Click()
		{
			await base.Click();
			if(this.For != null)
				return await this.For.Click();
			return ClickResult.SucceededNoOp;
		}
		HtmlElement _for = null;
		public HtmlElement For
		{
			get
			{
				if (_for == null)
				{
					string id = this.Element.GetAttributeCI("for");
					if (id == null) return null;
					var element = this.Element.Document.Descendants().Where(e => e.GetAttributeCI("id") == id).FirstOrDefault();
					if (element == null) return null;
					_for = OwningBrowser.CreateHtmlElement<HtmlElement>(element);
				}
				return _for;
			}
		}

	}
}
