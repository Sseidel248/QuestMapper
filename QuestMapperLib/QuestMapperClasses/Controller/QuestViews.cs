using QuestMapperLib.Exceptions;
using System.Collections.Generic;

namespace QuestMapperLib.Controller
{
    public class QuestViews
    {
        private Dictionary<int, List<int>> _viewMapping;

        public QuestViews()
        {
            _viewMapping = new Dictionary<int, List<int>>();
        }

        private int FindNextFreeId()
        {
            int nextId = 0;
            while (_viewMapping.ContainsKey(nextId))
            {
                nextId++;
            }
            return nextId;
        }

        public bool ExistView(int viewId)
        {
            return _viewMapping.ContainsKey(viewId);
        }

        public bool ExistQuestInView(int viewId, int questId)
        {
            if (!ExistView(viewId))
                return false;

            List<int> values = _viewMapping[viewId];
            foreach (int id in values)
            {
                if (id == questId)
                    return true;
            }
            return false;
        }

        public bool ExistQuest(int questId)
        {
            foreach (int viewId in _viewMapping.Keys)
            {
                if (ExistQuestInView(viewId, questId))
                {
                    return true;
                }
            }
            return false;
        }

        public void AddQuestToView(int viewId, int questId)
        {
            if (ExistQuestInView(viewId, questId))
            {
                throw new QuestAlreadyExistException(viewId, questId);
            }

            if (!ExistView(viewId))
            {
                AddView(viewId);
            }
            _viewMapping[viewId].Add(questId);
        }

        public void RemoveQuestFromView(int viewId, int questId)
        {
            if (!ExistView(viewId))
            {
                throw new ViewNotFoundException(viewId);
            }
            _viewMapping[viewId].Remove(questId);
        }

        public void RemoveQuestFromAllViews(int questId)
        {
            foreach (int viewId in _viewMapping.Keys)
            {
                if (ExistQuestInView(viewId, questId))
                {
                    RemoveQuestFromView(viewId, questId);
                }
            }
        }

        public int AddView(List<int> questIds)
        {
            int viewId = FindNextFreeId();
            if (questIds == null)
            {
                questIds = new List<int>();
            }
            _viewMapping.Add(viewId, questIds);
            return viewId;
        }

        public int AddView()
        {
            int viewId = FindNextFreeId();
            _viewMapping.Add(viewId, new List<int>());
            return viewId;
        }

        public bool AddView(int viewId)
        {
            return _viewMapping.TryAdd(viewId, new List<int>());
        }

        public void RemoveView(int viewId)
        {
            if (!ExistView(viewId))
            {
                throw new ViewNotFoundException(viewId);
            }
            _viewMapping.Remove(viewId);
        }

        public List<int> GetAllQuestsForView(int viewId)
        {
            if (ExistView(viewId))
            {
                return _viewMapping[viewId];
            }
            throw new ViewNotFoundException(viewId);
        }

        public Dictionary<int, List<int>> GetAllViews()
        {
            return new Dictionary<int, List<int>>(_viewMapping);
        }

        public List<int> GetAllViewIds()
        {
            return new List<int>(_viewMapping.Keys);
        }
    }
}
