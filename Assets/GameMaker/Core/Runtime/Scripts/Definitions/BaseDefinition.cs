using UnityEngine;


namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public abstract class BaseDefinition : Definition,
                                    ITitle,
                                    IIcon,
                                    IDescription,
                                    IMetaData
    {
        
        [SerializeField]
        private string _title = "";

        [SerializeField] 
        private Sprite _icon;

        [SerializeField]
        private string _description = "";

        [SerializeField]
        private BaseMetaData _metaData;
        [SerializeField]
        private BaseLocalizeMiddleware _localizeMiddleware;

        public BaseLocalizeMiddleware LocalizeMiddleware
        {
            get => _localizeMiddleware;
        }
        protected BaseDefinition() : base()
        {
            _title = "";
        }

        public BaseDefinition(string id, string name, string title, string description, Sprite icon, BaseMetaData metaData):base(id, name)
        {
            this._title = title;
            _description = description;
            _icon = icon;
            _metaData = metaData;
        }

        public virtual string GetTitle()
        {
            if(_localizeMiddleware != null)
            {
                return _localizeMiddleware.LocalizeText(_title);
            }
            return _title;
        }

        public virtual Sprite GetIcon()
        {   
            if(_localizeMiddleware != null)
            {
                return _localizeMiddleware.LocalizeIcon(_icon.name);
            }
            return _icon;
        }


        public virtual string GetDescription()
        {
            if(_localizeMiddleware != null)
            {
                return _localizeMiddleware.LocalizeText(_description);
            }
            return _description;
        }

        public BaseMetaData GetMetaData()
        {
            return _metaData;
        }

        public T GetMetaData<T>() where T : BaseMetaData
        {
            return _metaData as T;
        }
    }
}
