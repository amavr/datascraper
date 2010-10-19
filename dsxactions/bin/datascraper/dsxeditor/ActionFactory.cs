using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DataScraper
{
    class ActionFactory
    {
        public static ScriptAction Load(string fileName)
        {
            ScriptAction action = null;

            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            XmlNodeList nodes = doc.DocumentElement.SelectNodes(ScriptConsts.TAG_NAME);
            if (nodes.Count > 0)
                action = ParseXmlAction(nodes[0] as XmlElement);

            return action;
        }

        private static ScriptAction ParseXmlAction(XmlElement xmlAction)
        {
            ScriptAction action = null;
            switch (xmlAction.GetAttribute(ScriptConsts.ATTR_TYPE))
            {
                case ScriptConsts.TYPE_TEXT:
                    action = new ScriptTextAction();
                    break;

                case ScriptConsts.TYPE_DATE:
                    action = new ScriptDateAction();
                    break;

                case ScriptConsts.TYPE_LOAD:
                    action = new ScriptLoadAction();
                    break;

                case ScriptConsts.TYPE_DOWNLOAD:
                    action = new ScriptDownloadAction();
                    break;

                case ScriptConsts.TYPE_SAVE:
                    action = new ScriptSaveAction();
                    break;

                case ScriptConsts.TYPE_FIND:
                    action = new ScriptFindAction();
                    break;

                case ScriptConsts.TYPE_REPL:
                    action = new ScriptReplaceAction();
                    break;

                case ScriptConsts.TYPE_SET_VAR:
                    action = new ScriptSetVarAction();
                    break;

                case ScriptConsts.TYPE_GET_VAR:
                    action = new ScriptGetVarAction();
                    break;

                case ScriptConsts.TYPE_NEXT_PAGE:
                    action = new ScriptNextURLAction();
                    break;

            }
            // Устанавливаются свойства действия
            action.Load(xmlAction);

            foreach (XmlNode child in xmlAction.SelectNodes(ScriptConsts.TAG_NAME))
                action.Actions.Add(ParseXmlAction(child as XmlElement));

            return action;
        }
    }
}
