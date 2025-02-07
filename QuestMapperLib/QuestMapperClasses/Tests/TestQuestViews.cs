using QuestMapperLib.Controller;
using QuestMapperLib.Exceptions;

namespace Tests
{
    public class TestQuestViews
    {
        QuestViews QuestViews;
        public TestQuestViews()
        {
            QuestViews = new QuestViews();
        }

        [Fact]
        public void TestAddQuestToView_ExistingView()
        {
            // Arrange
            int viewId = QuestViews.AddView(new List<int>{ 1, 2, 3 });
            int questId = 4;

            // Act
            QuestViews.AddQuestToView(viewId, questId);
            var result = QuestViews.GetAllQuestsForView(viewId);

            // Assert
            Assert.Contains(questId, result);
        }

        [Fact]
        public void TestAddQuestToView_EmptyView()
        {
            // Arrange
            int viewId = QuestViews.AddView(new List<int>());

            // Act
            int questId = 1;
            QuestViews.AddQuestToView(viewId, questId);
            List<int> result = QuestViews.GetAllQuestsForView(viewId);

            // Assert
            Assert.Contains(1, result);
        }

        [Fact]
        public void TestAddeQuestsToView_MultipleAdd()
        {
            // Arrange
            int viewId = QuestViews.AddView(new List<int>{ 1, 2 });

            // Act
            QuestViews.AddQuestToView(viewId, 3);
            QuestViews.AddQuestToView(viewId, 4);
            var result = QuestViews.GetAllQuestsForView(viewId);

            // Assert
            Assert.Contains(3, result);
            Assert.Contains(4, result);
        }

        [Fact]
        public void TestAddQuestToView_Duplicate()
        {
            // Arrange
            int viewId = QuestViews.AddView(new List<int>{ 1, 2, 3 });

            // Act & Assert
            Assert.Throws<QuestAlreadyExistException>(() => 
            {
                QuestViews.AddQuestToView(viewId, 3);
            });
        }
    }
}
