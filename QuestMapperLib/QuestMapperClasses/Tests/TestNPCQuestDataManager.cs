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
            manager = new NPCQuestDataManager();
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
        public void TestAddNPC_InvalidInput()
        {
            Assert.Throws<ValueToAddIsNullException>(() =>
            {
                manager.AddNPC(null);
            });       
        }

        [Fact]
        public void TestAddNPC_DuplicateID()
        {
            NPC npc1 = new NPC(1);
            NPC npc2 = new NPC(1);

            manager.AddNPC(npc1);

            Assert.False(manager.AddNPC(npc2));
        }

        [Fact]
        public void TestRemoveNPC_ExistingNPC()
        {
            NPC npc = new NPC(1);
            manager.AddNPC(npc);
    
            manager.RemoveNPC(npc.Id);
    
            Assert.False(manager.ExistNPC(npc.Id));
        }

        [Fact]
        public void TestRemoveNPC_NonExistingNPC()
        {   
            manager.RemoveNPC(1);
    
            Assert.False(manager.ExistNPC(1));
        }

        [Fact]
        public void TestGetNPC_ExistingNPC()
        {
            INPC expectedNpc = new NPC(1);
            manager.AddNPC(expectedNpc);
    
            INPC actualNpc = manager.GetNPC(expectedNpc.Id);
    
            Assert.Equal(expectedNpc, actualNpc);
        }

        [Fact]
        public void TestGetNPC_NonExistingNPC()
        {    
            INPC actualNpc = manager.GetNPC(1);
    
            Assert.Equal(NullNPC.Instance, actualNpc);
        }

        [Fact]
        public void TestRenameNPC_ExistingNPC()
        {
            NPC npc = new NPC(1);
            npc.Name = "Old Name";
            manager.AddNPC(npc);
    
            manager.RenameNPC(npc.Id, "New Name");
    
            Assert.Equal("New Name", manager.GetNPC(npc.Id).Name);
        }

        //[Fact]
        //public void TestRenameNPC_DefaultNPC()
        //{   
        //    manager.RenameNPC(1, "New Name");
    
        //    Assert.Empty(manager.GetAllNPCs());
        //    Assert.Equal("Default NPC", NPC.Default.Name);
        //}

        [Fact]
        public void TestUpdateNPCDescription_ExistingNPC()
        {
            NPC npc = new NPC(1);
            npc.Name = "Old Name";
            npc.Description = "abc";
            manager.AddNPC(npc);
    
            manager.UpdateNPCDescription(npc.Id, "def");
    
            Assert.Equal("def", manager.GetNPC(npc.Id).Description);
        }

        //[Fact]
        //public void TestUpdateNPCDescription_NonExistingNPC()
        //{   
        //    manager.UpdateNPCDescription(1, "def");
    
        //    Assert.Empty(manager.GetAllNPCs());
        //    Assert.Equal("I am the Default NPC.", NPC.Default.Description);
        //}

        [Fact]
        public void TestGetAllNPCs_Valid()
        {
            NPC n1 = new NPC(1);
            n1.Name = "NPC 1";
            NPC n2 = new NPC(2);
            n2.Name = "NPC 2";

            manager.AddNPC(n1);
            manager.AddNPC(n2);
    
            List<INPC> allNPCs = manager.GetAllNPCs();
    
            Assert.Equal(2, allNPCs.Count);
            Assert.Equal("NPC 1", allNPCs[0].Name);
            Assert.Equal("NPC 2", allNPCs[1].Name);
        }

        [Fact]
        public void TestGetAllNPCs_Empty()
        {   
            List<INPC> allNPCs = manager.GetAllNPCs();
    
            Assert.Empty(allNPCs);
        }

        [Fact]
        public void TestExistQuest_Valid()
        {
            Quest quest = new Quest(1);
            int viewId = 1;
            manager.AddQuest(quest, viewId);

            Assert.True(manager.ExistQuest(quest.Id));
            Assert.True(manager.ExistQuestInView(quest.Id, viewId));           
        }

        [Fact]
        public void TestExistQuestInView_Valid()
        {
            Quest quest = new Quest(1);
            int viewId = 1;
            manager.AddQuest(quest, viewId);

            Assert.True(manager.ExistQuest(quest.Id));
            Assert.True(manager.ExistQuestInView(quest.Id, viewId));
        }

        [Fact]
        public void TestExistQuest_Invalid()
        {
            Assert.False(manager.ExistQuest(1));
        }

        [Fact]
        public void TestExistPreQuestInQuest_Valid()
        {
            Quest q1 = new Quest(1);
            Quest q2 = new Quest(2);
            int viewId = 1;
            manager.AddQuest(q1, viewId);
            manager.AddQuest(q2, viewId);
            manager.AddPreQuestToQuest(q1.Id, q2.Id);

            Assert.True(manager.ExistPreQuestInQuest(q1.Id, q2.Id));
            Assert.True(manager.ExistQuestInView(q1.Id, viewId));
            Assert.True(manager.ExistQuestInView(q2.Id, viewId));
        }

        [Fact]
        public void TestExistPreQuestInQuest_Invalid()
        {
            Assert.False(manager.ExistPreQuestInQuest(1, 2));
        }

        [Fact]
        public void TestAddQuest_ValidInput()
        {
            Quest quest = new Quest(1);
            quest.Name = "MQ-1";

            manager.AddQuest(quest, -1);

            Assert.True(manager.ExistQuest(quest.Id));
            Assert.Equal("MQ-1", manager.GetQuest(quest.Id).Name);
        }

        [Fact]
        public void TestAddQuest_InvalidInput()
        {
            int viewId = 1;
            Assert.Throws<ValueToAddIsNullException>(() =>
            { 
               manager.AddQuest(null, viewId); 
            });        
        }

        [Fact]
        public void TestAddQuest_DuplicateID()
        {
            Quest q1 = new Quest(1);
            Quest q2 = new Quest(1);
            int viewId = 1;
            manager.AddQuest(q1, viewId);

            Assert.False(manager.AddQuest(q2, viewId));
        }

        [Fact]
        public void TestRemoveQuest_ExistingQuest()
        {
            Quest quest = new Quest(1);
            int viewId = 1;
            manager.AddQuest(quest, viewId);
    
            manager.RemoveQuest(quest.Id);
    
            Assert.False(manager.ExistQuest(quest.Id));
            Assert.False(manager.ExistQuestInView(quest.Id, viewId));
        }

        [Fact]
        public void TestRemoveQuest_NonExistingQuest()
        {   
            manager.RemoveQuest(1);
    
            Assert.False(manager.ExistQuest(1));
        }

        [Fact]
        public void TestRemoveQuest_ExistingPreQuests()
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
        public void TestGetQuest_ExistingQuest()
        {
            IQuest expectedQuest = new Quest(1);
            int viewId = 1;
            manager.AddQuest(expectedQuest, viewId);
    
            IQuest actualQuest = manager.GetQuest(expectedQuest.Id);
    
            Assert.Equal(expectedQuest, actualQuest);
            Assert.True(manager.ExistQuestInView(expectedQuest.Id, viewId));
        }

        [Fact]
        public void TestGetQuest_NonExistingQuest()
        {    
            IQuest actualQuest = manager.GetQuest(1);
    
            Assert.Equal(NullQuest.Instance, actualQuest);
            Assert.False(manager.ExistQuest(1));
        }

        [Fact]
        public void TestRenameQuest_ExistingQuest()
        {
            Quest quest = new Quest(1);
            quest.Name = "Old Name";
            int viewId = 1;
            manager.AddQuest(quest, viewId);
    
            manager.RenameQuest(quest.Id, "New Name");
    
            Assert.Equal("New Name", manager.GetQuest(quest.Id).Name);
            Assert.True(manager.ExistQuestInView(quest.Id, viewId));
        }

        //[Fact]
        //public void TestRenameQuest_DefaultQuest()
        //{   
        //    manager.RenameQuest(1, "New Name");
    
        //    Assert.Empty(manager.GetAllQuests());
        //    Assert.Equal("Default Quest", Quest.Default.Name);
        //}

        [Fact]
        public void TestUpdateQuestDescription_ExistingQuest()
        {
            Quest quest = new Quest(1);
            quest.Name = "Old Name";
            quest.Description = "abc";
            int viewId = 1;
            manager.AddQuest(quest, viewId);
    
            manager.UpdateQuestDescription(quest.Id, "def");
    
            Assert.Equal("def", manager.GetQuest(quest.Id).Description);
        }

        //[Fact]
        //public void TestUpdateQuestDescription_NonExistingQuest()
        //{   
        //    manager.UpdateQuestDescription(1, "def");
    
        //    Assert.Empty(manager.GetAllQuests());
        //    Assert.Equal("Default Questdescription", Quest.Default.Description);
        //}

        [Fact]
        public void TestUpdateQuestReward_ExistingQuest()
        {
            Quest quest = new Quest(1);
            quest.Name = "Old Name";
            quest.Reward = "abc";
            int viewId = 1;
            manager.AddQuest(quest, viewId);
    
            manager.UpdateQuestReward(quest.Id, "def");
    
            Assert.Equal("def", manager.GetQuest(quest.Id).Reward);
        }

        //[Fact]
        //public void TestUpdateNQuestReward_NonExistingQuest()
        //{   
        //    manager.UpdateQuestReward(1, "def");
    
        //    Assert.Empty(manager.GetAllNPCs());
        //    Assert.Equal("Default Reward", Quest.Default.Reward);
        //}

        [Fact]
        public void TestUpdateQuestStartConnection_ExistingQuest()
        {
            Quest quest = new Quest(1);
            int viewId = 1;
            manager.AddQuest(quest, viewId);
    
            manager.UpdateQuestStartConnection(quest.Id, true);
    
            Assert.True(manager.GetQuest(quest.Id).IsConnectedWithStart);
        }

        //[Fact]
        //public void TestUpdateNQuestStartConnection_NonExistingQuest()
        //{   
        //    manager.UpdateQuestStartConnection(1, true);
    
        //    Assert.Empty(manager.GetAllNPCs());
        //    Assert.False(Quest.Default.IsConnectedWithStart);
        //}

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

        [Fact]
        public void TestAddPreQuestToQuest_Invalid()
        {
            Quest q2 = new Quest(2);
            int viewId = 1;
            manager.AddQuest(q2, viewId);

            manager.AddPreQuestToQuest(1, q2.Id);

            Assert.False(manager.ExistPreQuestInQuest(q2.Id, 1));
            Assert.Equal(NullQuest.Instance, manager.GetQuest(1));
            Assert.Empty(manager.GetQuest(q2.Id).PreQuestIds);
            Assert.Single(manager.GetAllQuests());
        }

        [Fact]
        public void TestAddPreQuestToQuest_Duplicate()
        {
            Quest q1 = new Quest(1);
            Quest q2 = new Quest(2);
            int viewId = 1;
            manager.AddQuest(q1, viewId);
            manager.AddQuest(q2, viewId);

            manager.AddPreQuestToQuest(q1.Id, q2.Id);
            manager.AddPreQuestToQuest(q1.Id, q2.Id);

            Assert.True(manager.ExistPreQuestInQuest(q1.Id, q2.Id));
            Assert.Equal(2, manager.GetAllQuests().Count);
            Assert.Single(manager.GetQuest(q2.Id).PreQuestIds);
        }

    }
}
