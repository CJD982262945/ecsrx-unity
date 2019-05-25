using System.Linq;
using Unidux;

namespace FGUI
{
    public enum ActionType
    {
        ADD_TODO,
        TOGGLE_TODO,
    }

    public class FAction
    {
        public ActionType ActionType;
//        public Todo Todo;
    }
    
    public static class FGUIHelloWorld
    {
        public class Reducer : ReducerBase<State, FAction>
        {
            public override State Reduce(State state, FAction action)
            {
                switch (action.ActionType)
                {
                    case ActionType.ADD_TODO:
//                        state.Todo = AddTodo(state.Todo, action.Todo.Text);
                        return state;
                    case ActionType.TOGGLE_TODO:
//                        state.Todo = ToggleTodo(state.Todo, action.Todo);
                        return state;
                }

                return state;
            }
        }
    }
}