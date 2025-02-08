using QuestMapperLib.Controller;
using QuestMapperLib.Model;
using QuestMapperLib.Exceptions;

namespace Tests
{
    public class TestNPCQuestDataManager
    {
        NPCQuestDataManager manager;

        public TestNPCQuestDataManager()
        {
            manager = NPCQuestDataManager.Instance;
        }

        [Fact]
        public void AddNPC_ValidNPC_ShouldReturnTrue()
        {
            NPC npc = new NPC(1);
            npc.Name = "John";

            Assert.True(manager.AddNPC(npc));
            Assert.True(manager.ExistNPC(npc.Id));
            Assert.Equal("John", manager.GetNPC(npc.Id).Name);
        }

        [Fact]
        public void AddNPC_NonExsitingNPC_ShouldReturnFalse()
        {
            Assert.False(manager.ExistNPC(1));
        }

        [Fact]
        public void AddNPC_DuplicateNPC_ShouldReturnFalse()
        {

            NPC npc = new NPC(1) { Name = "Test NPC" };

            manager.AddNPC(npc);
            var result = manager.AddNPC(npc);

            Assert.False(result);
        }

        [Fact]
        public void GetNPC_NonExistentId_ShouldReturnDefault()
        {
            INPC npc = manager.GetNPC(999);

            Assert.Equal(NullNPC.Instance, npc);
        }

        [Fact]
        public void RemoveNPC_ValidId_ShouldNotExistAnymore()
        {
            INPC npc = new NPC(1) { Name = "Test NPC" };
            manager.AddNPC(npc);

            manager.RemoveNPC(1);

            Assert.False(manager.ExistNPC(1));
        }

        [Fact]
        public void RenameNPC_ValidId_ShouldUpdateName()
        {
            INPC npc = new NPC(1) { Name = "Old Name" };
            manager.AddNPC(npc);

            manager.RenameNPC(1, "New Name");

            Assert.Equal("New Name", manager.GetNPC(1).Name);
        }

        [Fact]
        public void UpdateNPCDescription_ValidId_ShouldUpdateDescription()
        {
            INPC npc = new NPC(1) { Description = "Old Description" };
            manager.AddNPC(npc);

            manager.UpdateNPCDescription(1, "New Description");

            Assert.Equal("New Description", manager.GetNPC(1).Description);
        }

        [Fact]
        public void AddQuest_ValidQuest_ShouldReturnTrue()
        {
            IQuest quest = new Quest(1) { Name = "First Quest" };

            var result = manager.AddQuest(quest, 0);

            Assert.True(result);
            Assert.True(manager.ExistQuest(1));
        }

        [Fact]
        public void AddQuest_DuplicateQuest_ShouldReturnFalse()
        {
            IQuest quest = new Quest(1) { Name = "First Quest" };

            manager.AddQuest(quest, 0);
            var result = manager.AddQuest(quest, 0);

            Assert.False(result);
        }

        [Fact]
        public void GetQuest_NonExistentId_ShouldReturnNullQuest()
        {
            IQuest quest = manager.GetQuest(999);

            Assert.Equal(NullQuest.Instance, quest);
        }

        [Fact]
        public void RemoveQuest_ValidId_ShouldNotExistAnymore()
        {
            IQuest quest = new Quest(1) { Name = "First Quest" };
            manager.AddQuest(quest, 0);

            manager.RemoveQuest(1);

            Assert.False(manager.ExistQuest(1));
        }

        [Fact]
        public void RenameQuest_ValidId_ShouldUpdateName()
        {
            IQuest quest = new Quest(1) { Name = "Old Quest Name" };
            manager.AddQuest(quest, 0);

            manager.RenameQuest(1, "New Quest Name");

            Assert.Equal("New Quest Name", manager.GetQuest(1).Name);
        }

        [Fact]
        public void UpdateQuestDescription_ValidId_ShouldUpdateDescription()
        {
            IQuest quest = new Quest(1) { Description = "Old Description" };
            manager.AddQuest(quest, 0);

            manager.UpdateQuestDescription(1, "New Description");

            Assert.Equal("New Description", manager.GetQuest(1).Description);
        }

        [Fact]
        public void UpdateQuestReward_ValidId_ShouldUpdateReward()
        {
            IQuest quest = new Quest(1) { Reward = "Old Reward" };
            manager.AddQuest(quest, 0);

            manager.UpdateQuestReward(1, "New Reward");

            Assert.Equal("New Reward", manager.GetQuest(1).Reward);
        }

        [Fact]
        public void AddPreQuestToQuest_ValidPreQuest_ShouldBeAdded()
        {
            IQuest quest = new Quest(1) { Name = "Main Quest" };
            IQuest preQuest = new Quest(2) { Name = "Pre Quest" };

            manager.AddQuest(quest, 0);
            manager.AddQuest(preQuest, 0);
            manager.AddPreQuestToQuest(2, 1);

            Assert.Contains(2, manager.GetQuest(1).PreQuestIds);
        }

        [Fact]
        public void GetAllNPCs_ShouldReturnCorrectCount()
        {
            INPC npc1 = new NPC(1) { Name = "NPC1" };
            INPC npc2 = new NPC(2) { Name = "NPC2" };

            manager.AddNPC(npc1);
            manager.AddNPC(npc2);

            var npcs = manager.GetAllNPCs();

            Assert.Equal(2, npcs.Count);
        }

        [Fact]
        public void GetAllQuests_ShouldReturnCorrectCount()
        {
            IQuest quest1 = new Quest(1) { Name = "Quest1" };
            IQuest quest2 = new Quest(2) { Name = "Quest2" };

            manager.AddQuest(quest1, 0);
            manager.AddQuest(quest2, 0);

            var quests = manager.GetAllQuests();

            Assert.Equal(2, quests.Count);
        }

        [Fact]
        public void ExistValidQuestInView()
        {
            IQuest quest = new Quest(1);
            int viewId = 1;
            manager.AddQuest(quest, viewId);

            Assert.True(manager.ExistQuestInView(quest.Id, viewId));
        }

        [Fact]
        public void ExistInvalidPreQuestInQuest()
        {
            Assert.False(manager.ExistPreQuestInQuest(1, 2));
        }


        [Fact]
        public void AddNullAsQuest_InvalidInput()
        {
            int viewId = 1;
            Assert.Throws<ValueToAddIsNullException>(() =>
            { 
               manager.AddQuest(null, viewId); 
            });        
        }




        [Fact]
        public void RemoveQuestAndHisPreQuest()
        {
            Quest q1 = new Quest(1);
            Quest q2 = new Quest(2);
            int viewId = 1;
            manager.AddQuest(q1, viewId);
            manager.AddQuest(q2, viewId);
            manager.AddPreQuestToQuest(q1.Id, q2.Id);
    
            manager.RemoveQuest(q1.Id);
    
            Assert.False(manager.ExistQuest(q1.Id));
            Assert.False(manager.ExistPreQuestInQuest(q1.Id, q2.Id));
            Assert.False(manager.ExistQuestInView(q1.Id, viewId));
        }

        [Fact]
        public void UpdateValidQuestStartConnection()
        {
            Quest quest = new Quest(1);
            int viewId = 1;
            manager.AddQuest(quest, viewId);
    
            manager.UpdateQuestStartConnection(quest.Id, true);
    
            Assert.True(manager.GetQuest(quest.Id).IsConnectedWithStart);
        }

        [Fact]
        public void TestAddPreQuestToQuest_Valid()
        {
            Quest q1 = new Quest(1);
            Quest q2 = new Quest(2);
            int viewId = 1;
            manager.AddQuest(q1, viewId);
            manager.AddQuest(q2, viewId);

            manager.AddPreQuestToQuest(q1.Id, q2.Id);

            Assert.True(manager.ExistPreQuestInQuest(q1.Id, q2.Id));
            Assert.Single(manager.GetQuest(q2.Id).PreQuestIds);
            Assert.Equal(2, manager.GetAllQuests().Count);
        }
    }
}
