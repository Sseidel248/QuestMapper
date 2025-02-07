using QuestMapperLib.Model;
using QuestMapperLib.Exceptions;
using System.Collections.Generic;

namespace QuestMapperLib.Controller
{
    public class NPCQuestDataManager
    {
        private int _nextNpcId = 0;
        private Dictionary<int, INPC> _npcs;
        private int _nextQuestId = 0;
        private Dictionary<int, IQuest> _quests;
        private int _nextViewId = 0;
        private QuestViews _questViews;

        public NPCQuestDataManager()
        {
            _npcs = new Dictionary<int, INPC>();
            _quests = new Dictionary<int, IQuest>();
            _questViews = new QuestViews();
        }

        private bool IsQuestValid(IQuest quest)
        {
            return quest != NullQuest.Instance;
        }

        private bool IsNPCValid(INPC npc)
        {
            return npc != NullNPC.Instance;
        }

        public bool ExistNPC(int npcId)
        {
            return _npcs.ContainsKey(npcId);
        }

        public bool AddNPC(INPC npc)
        {
            if (npc == null || npc == NullNPC.Instance)
            {
                throw new ValueToAddIsNullException(typeof(IQuest).Name);
            }

            if (npc.Id <= 0 && npc is NPC realNPC)
            {
                int id = _nextNpcId;
                realNPC.Id = id;
                _nextNpcId++;
            }

            if (ExistNPC(npc.Id))
            {
                return false;
            }

            return _npcs.TryAdd(npc.Id, npc);
        }

        public void RemoveNPC(int id)
        {
            if (ExistNPC(id))
            {
                _npcs.Remove(id);
            }
        }

        public INPC GetNPC(int id)
        {
            if (ExistNPC(id))
            {
                return _npcs[id];
            }
            else
            {
                return NullNPC.Instance;
            }
        }

        public void RenameNPC(int Id, string newName)
        {
            INPC npc = GetNPC(Id);
            if (IsNPCValid(npc))
            {
                npc.Name = newName;
            }
        }

        public void UpdateNPCDescription(int npcId, string newDescription)
        {
            INPC npc = GetNPC(npcId);
            if (IsNPCValid(npc))
            {
                npc.Description = newDescription;
            }
        }

        public List<INPC> GetAllNPCs()
        {
            return new List<INPC>(_npcs.Values);
        }

        public bool ExistQuest(int id)
        {
            return _quests.ContainsKey(id);
        }

        public bool ExistPreQuestInQuest(int preQuestId, int actQuestId)
        {
            return GetQuest(actQuestId).PreQuestIds.Contains(preQuestId);
        }

        public bool ExistQuestInView(int questId, int viewId)
        {
            return _questViews.ExistQuestInView(viewId, questId);
        }

        public bool ExistView(int id)
        {
            return _questViews.ExistView(id);
        }

        //TODO: AddQuest(Quest quest) -> dient zum Hinzufügen zur Datenliste

        public bool AddQuest(IQuest quest, int viewId)
        {
            if (quest == null || quest == NullQuest.Instance)
            {
                throw new ValueToAddIsNullException(typeof(Quest).Name);
            }

            if (quest.Id <= 0 && quest is Quest realQuest)
            {
                int id = _nextQuestId;
                realQuest.Id = id;
                _nextQuestId++;
            }
            //Falls die Quest noch nicht in der View existiert, füge sie hinzu
            if (!_questViews.ExistQuestInView(viewId, quest.Id))
            {
                //Existiert die View noch nicht, dann erzeuge sie
                if (!_questViews.ExistView(viewId))
                {
                    _questViews.AddView(viewId);
                }
                _questViews.AddQuestToView(viewId, quest.Id);
            }

            if (ExistQuest(quest.Id))
            {
                return false;
            }
            //Wenn die Quest noch nicht in der Datenablage exitiert, füge sie hinzu
            return _quests.TryAdd(quest.Id, quest);
        }

        public void RemoveQuest(int questId)
        {
            if (ExistQuest(questId))
            {
                _quests.Remove(questId);
                //Alle Abhängigkeiten von der gelöschten Quest beseitigen
                foreach (Quest q in _quests.Values)
                {
                    q.PreQuestIds.RemoveAll(aID => aID == questId);
                }
            }
            //Entferne auch alle anderen Questverweise in den Views
            if (_questViews.ExistQuest(questId))
            {
                _questViews.RemoveQuestFromAllViews(questId);
            }
        }

        public IQuest GetQuest(int id)
        {
            if (ExistQuest(id))
            {
                return _quests[id];
            }
            else
            {
                return NullQuest.Instance;
            }
        }
        public void RenameQuest(int questId, string newName)
        {
            IQuest quest = GetQuest(questId);
            if (IsQuestValid(quest))
            {
                quest.Name = newName;
            }
        }

        public void UpdateQuestDescription(int questId, string newDescription)
        {
            IQuest quest = GetQuest(questId);
            if (IsQuestValid(quest))
            {
                quest.Description = newDescription;
            }
        }

        public void UpdateQuestReward(int questId, string newReward)
        {
            IQuest quest = GetQuest(questId);
            if (IsQuestValid(quest))
            {
                quest.Reward = newReward;
            }
        }

        public void UpdateQuestStartConnection(int questId, bool connectedToStart)
        {
            IQuest quest = GetQuest(questId);
            if (IsQuestValid(quest))
            {
                quest.IsConnectedWithStart = connectedToStart;
            }
        }

        public void AddPreQuestToQuest(int preQuestId, int actQuestId)
        {
            IQuest quest = GetQuest(actQuestId);
            if (quest == NullQuest.Instance)
                return;

            if (!ExistQuest(preQuestId))
                return;

            if (!ExistPreQuestInQuest(preQuestId, actQuestId))
            {
                quest.PreQuestIds.Add(preQuestId);
            }
        }

        public List<IQuest> GetAllQuests()
        {
            return new List<IQuest>(_quests.Values);
        }
    }

}
