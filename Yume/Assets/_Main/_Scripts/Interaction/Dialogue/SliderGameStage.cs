using System;

public class SliderGameStage
{
    public readonly float TargetBoxElementSize;
    public readonly float TargetBoxElementHalfSize;
    public readonly float TargetBoxSpawnPosition;

    private Action _onWin;
    private Action _onLose;

    public SliderGameStage(float size)
    {
        TargetBoxElementSize = size;
        TargetBoxElementHalfSize = size / 2f;
        var margin = 0.05f;
        TargetBoxSpawnPosition = UnityEngine.Random.Range(TargetBoxElementHalfSize + margin, 1 - TargetBoxElementHalfSize - margin);
    }

    public void Execute(Action onWin, Action onLose, float cursorPosition)
    {
        _onWin = onWin;
        _onLose = onLose;

        var TargetBoxLeftSizePosition = TargetBoxSpawnPosition - TargetBoxElementHalfSize;
        var TargetBoxRightSidePosition = TargetBoxSpawnPosition + TargetBoxElementHalfSize;
        if (cursorPosition >= TargetBoxLeftSizePosition && cursorPosition <= TargetBoxRightSidePosition)
        {
            _onWin?.Invoke();
            return;
        }

        _onLose?.Invoke();
    }
}