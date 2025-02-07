using QuestMapperLib.Model;
using QuestMapperLib.Exceptions;

namespace Tests
{
    public class TestQuest
    {
        [Fact]
        public void TestQuestConnectedWithStart()
        {
            Quest quest = new Quest(1);
            Assert.False(quest.IsConnectedWithStart);
            quest.IsConnectedWithStart = true;
            Assert.True(quest.IsConnectedWithStart);
        }

        [Fact]
        public void TestQuestId()
        {
            Quest quest = new Quest(1);
            Assert.Equal(1, quest.Id);
        }       

        [Fact]
        public void TestQuestName()
        {
            Quest quest = new Quest(1);
            quest.Name = "Test Quest";
            Assert.Equal("Test Quest", quest.Name);
        }

        [Fact]
        public void TestQuestDescription()
        {
            Quest quest = new Quest(1);
            quest.Description = "Test Quest Description";
            Assert.Equal("Test Quest Description", quest.Description);
        }

        [Fact]
        public void TestQuestReward()
        {
            Quest quest = new Quest(1);
            quest.Reward = "Test Reward";
            Assert.Equal("Test Reward", quest.Reward);
        }

        [Fact]
        public void TestQuestPrerequisites()
        {
            Quest quest = new Quest(1);
            Quest preQuest = new Quest(2);
            quest.PreQuestIds.Add(preQuest.Id);
            Assert.Single(quest.PreQuestIds);
            Assert.Equal(2, quest.PreQuestIds[0]);
        }

        [Fact]
        public void TestDefaultQuest()
        {
            IQuest nullQuest = NullQuest.Instance;
            Assert.Equal(-1, nullQuest.Id);
            Assert.Empty(nullQuest.PreQuestIds);
            Assert.True(nullQuest.IsNull);
        }

        [Fact]
        public void TestQuestPrerequisitesList()
        {
            Quest quest = new Quest(1);
            Assert.Empty(quest.PreQuestIds);
        }

        [Fact]
        public void TestExceptionIfQuestNameIsEmpty()
        {
            Quest quest = new Quest(1);
            Assert.Throws<ValueIsNullOrEmptyException>(() =>
            {
                quest.Name = string.Empty;
            });
        }

        [Fact]
        public void TestExceptionIfQuestNameIfWhitespace()
        {
            Quest quest = new Quest(1);
            Assert.Throws<ValueIsNullOrEmptyException>(() =>
            {
                quest.Name = "  ";
            });   
        }

        [Fact]
        public void TestExceptionIfQuestDescriptionIsNull()
        {
            Quest quest = new Quest(1);
            Assert.Throws<ValueIsNullException>(() =>
            {
                quest.Description = null;
            });
        }

        [Fact]
        public void TestExceptionIfQuestRewardIsNull()
        {
            Quest quest = new Quest(1);
            Assert.Throws<ValueIsNullException>(() =>
            {
                quest.Reward = null;
            });
        }
    }
}