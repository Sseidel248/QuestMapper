using QuestMapperLib.Model;
using QuestMapperLib.Exceptions;

namespace Tests
{
    public class TestNPC
    {
        [Fact]
        public void TestNpcId()
        {
            NPC npc = new NPC(1);
            Assert.Equal(1, npc.Id);
        }

        [Fact]
        public void TestNpcName()
        {
            NPC npc = new NPC(1);
            npc.Name = "Test NPC";
            Assert.Equal("Test NPC", npc.Name);
        }

        [Fact]
        public void TestNpcDescription()
        {
            NPC npc = new NPC(1);
            npc.Description = "Test NPC Description";
            Assert.Equal("Test NPC Description", npc.Description);
        }

        //[Fact]
        //public void TestDefaultNpc()
        //{
        //    NPC defaultNpc = NPC.Default;
        //    Assert.Equal(-1, defaultNpc.Id);
        //    Assert.Equal("Default NPC", defaultNpc.Name);
        //    Assert.Equal("I am the Default NPC.", defaultNpc.Description);
        //}

        [Fact]
        public void TestExceptionIfNPCNameIsEmpty()
        {
            NPC npc = new NPC(1);
            Assert.Throws<ValueIsNullOrEmptyException>(() =>
            {
                npc.Name = string.Empty;
            });
            
        }

        [Fact]
        public void TestExceptionIfNPCNameIfWhitespace()
        {
            NPC npc = new NPC(1);
            Assert.Throws<ValueIsNullOrEmptyException>(() =>
            {
                npc.Name = "  ";
            });
        }

        [Fact]
        public void TestExceptionIfNPCDescriptionIsNull()
        {
            NPC npc = new NPC(1);
            Assert.Throws<ValueIsNullException>(() =>
            {
                npc.Description = null;
            });
        }
    }
}
