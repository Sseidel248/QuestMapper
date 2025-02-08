using QuestMapperLib.Controller;
using QuestMapperLib.Exceptions;

namespace Tests
{
    public class TestQuestViews
    {
        QuestViews questViews;
        public TestQuestViews()
        {
            questViews = QuestViews.Instance;
        }

        [Fact]
        public void AddView_ShouldCreateNewView()
        {
            int viewId = questViews.AddView();

            Assert.True(questViews.ExistView(viewId));
        }

        [Fact]
        public void AddView_WithGivenId_ShouldCreateView()
        {
            bool result = questViews.AddView(10);

            Assert.True(result);
            Assert.True(questViews.ExistView(10));
        }

        [Fact]
        public void AddView_WithQuestList_ShouldCreateViewWithQuests()
        {;
            var questIds = new List<int> { 1, 2, 3 };

            int viewId = questViews.AddView(questIds);

            Assert.True(questViews.ExistView(viewId));
            Assert.Equal(questIds, questViews.GetAllQuestsForView(viewId));
        }

        [Fact]
        public void RemoveView_ShouldRemoveExistingView()
        {
            int viewId = questViews.AddView();

            questViews.RemoveView(viewId);

            Assert.False(questViews.ExistView(viewId));
        }

        [Fact]
        public void RemoveView_NonExistentView_ShouldThrowException()
        {
            Assert.Throws<ViewNotFoundException>(() => questViews.RemoveView(999));
        }

        [Fact]
        public void AddQuestToView_Valid_ShouldAddQuest()
        {
            int viewId = questViews.AddView();
            int questId = 1;

            questViews.AddQuestToView(viewId, questId);

            Assert.True(questViews.ExistQuestInView(viewId, questId));
        }
           
        [Fact]
        public void AddQuestToView_Duplicate_ShouldThrowException()
        {
            int viewId = questViews.AddView();
            int questId = 1;

            questViews.AddQuestToView(viewId, questId);

            Assert.Throws<QuestAlreadyExistException>(() => questViews.AddQuestToView(viewId, questId));
        }

        [Fact]
        public void RemoveQuestFromView_ShouldRemoveQuest()
        {
            int viewId = questViews.AddView();
            int questId = 1;
            questViews.AddQuestToView(viewId, questId);

            questViews.RemoveQuestFromView(viewId, questId);

            Assert.False(questViews.ExistQuestInView(viewId, questId));
        }

        [Fact]
        public void RemoveQuestFromView_NonExistentView_ShouldThrowException()
        {
            Assert.Throws<ViewNotFoundException>(() => questViews.RemoveQuestFromView(999, 1));
        }

        [Fact]
        public void RemoveQuestFromAllViews_ShouldRemoveQuestFromEveryView()
        {
            int viewId1 = questViews.AddView();
            int viewId2 = questViews.AddView();
            int questId = 1;

            questViews.AddQuestToView(viewId1, questId);
            questViews.AddQuestToView(viewId2, questId);

            questViews.RemoveQuestFromAllViews(questId);

            Assert.False(questViews.ExistQuestInView(viewId1, questId));
            Assert.False(questViews.ExistQuestInView(viewId2, questId));
        }

        [Fact]
        public void ExistQuestInView_ShouldReturnTrue_WhenQuestExists()
        {
            int viewId = questViews.AddView();
            int questId = 1;
            questViews.AddQuestToView(viewId, questId);

            Assert.True(questViews.ExistQuestInView(viewId, questId));
        }

        [Fact]
        public void ExistQuestInView_ShouldReturnFalse_WhenQuestDoesNotExist()
        {
            int viewId = questViews.AddView();

            Assert.False(questViews.ExistQuestInView(viewId, 999));
        }

        [Fact]
        public void ExistQuest_ShouldReturnTrue_WhenQuestExistsInAnyView()
        {
            int viewId1 = questViews.AddView();
            int viewId2 = questViews.AddView();
            int questId = 1;
            questViews.AddQuestToView(viewId1, questId);

            Assert.True(questViews.ExistQuest(questId));
        }

        [Fact]
        public void ExistQuest_ShouldReturnFalse_WhenQuestDoesNotExist()
        {
            Assert.False(questViews.ExistQuest(999));
        }

        [Fact]
        public void GetAllQuestsForView_ShouldReturnCorrectQuestList()
        {
            int viewId = questViews.AddView();
            int questId1 = 1;
            int questId2 = 2;
            questViews.AddQuestToView(viewId, questId1);
            questViews.AddQuestToView(viewId, questId2);

            var quests = questViews.GetAllQuestsForView(viewId);

            Assert.Contains(questId1, quests);
            Assert.Contains(questId2, quests);
        }

        [Fact]
        public void GetAllQuestsForView_NonExistentView_ShouldThrowException()
        {
            Assert.Throws<ViewNotFoundException>(() => questViews.GetAllQuestsForView(999));
        }

        [Fact]
        public void GetAllViews_ShouldReturnCorrectData()
        {
            var questViews = new QuestViews();
            int viewId1 = questViews.AddView();
            int viewId2 = questViews.AddView();
            var allViews = questViews.GetAllViews();

            Assert.True(allViews.ContainsKey(viewId1));
            Assert.True(allViews.ContainsKey(viewId2));
        }

        [Fact]
        public void GetAllViewIds_ShouldReturnCorrectList()
        {
            var questViews = new QuestViews();
            int viewId1 = questViews.AddView();
            int viewId2 = questViews.AddView();
            var allViewIds = questViews.GetAllViewIds();

            Assert.Contains(viewId1, allViewIds);
            Assert.Contains(viewId2, allViewIds);
        }
    }
}
