using GameFramework;
using GameFramework.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// buff开始生效事件
/// </summary>
class BuffStartEventArgs : GameEventArgs
{
    public int playerId;
    public string key;
    public float value;

    public static readonly int EventId = typeof(BuffStartEventArgs).GetHashCode();

    public override int Id => EventId;

    public static BuffStartEventArgs Create(int playerId, string key, float value)
    {
        BuffStartEventArgs e = ReferencePool.Acquire<BuffStartEventArgs>();
        e.playerId = playerId;
        e.key = key;
        e.value = value;
        return e;
    }

    public override void Clear()
    {
        playerId = 0;
        key = null;
        value = 0;
    }
}
