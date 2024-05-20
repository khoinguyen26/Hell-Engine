using System.Collections.ObjectModel;
using System.Diagnostics;

namespace HellEditor.Utils
{
    interface IUndoRedo
    {
        string Name { get; }
        void Undo();
        void Redo();
    }

    public class UndoRedoAction : IUndoRedo
    {
        private Action _undo;
        private Action _redo;

        public string Name { get; }

        public void Redo() => _redo();

        public void Undo() => _undo();

        public UndoRedoAction(string name)
        {
            Name = name;
        }

        public UndoRedoAction(Action undo, Action redo, string name) : this(name)
        {
            Debug.Assert(undo != null && redo != null);
            _undo = undo;
            _redo = redo;
        }
    }

    class UndoRedo
    {
        // use ObservableCollection for undo redo history in the fure
        private readonly ObservableCollection<IUndoRedo> _redoList = [];
        private readonly ObservableCollection<IUndoRedo> _undoList = [];
        public ReadOnlyObservableCollection<IUndoRedo> RedoList { get; }
        public ReadOnlyObservableCollection<IUndoRedo> UndoList { get; }

        public void Reset()
        {
            _redoList.Clear();
            _undoList.Clear();
        }

        public UndoRedo()
        {
            RedoList = new ReadOnlyObservableCollection<IUndoRedo>(_redoList);
            UndoList = new ReadOnlyObservableCollection<IUndoRedo>(_undoList);
        }

        public void Add(IUndoRedo cmd)
        {
            _undoList.Add(cmd);
            _redoList.Clear();
        }

        public void Undo()
        {
            if (_undoList.Count > 0)
            {
                var cmd = _undoList.Last();
                _undoList.RemoveAt(_undoList.Count - 1);
                cmd.Undo();
                _redoList.Insert(0, cmd);
            }
        }

        public void Redo()
        {
            if (_redoList.Any())
            {
                var cmd = _redoList.First();
                _redoList.RemoveAt(0);
                cmd.Redo();
                _undoList.Add(cmd);
            }
        }
    }
}
