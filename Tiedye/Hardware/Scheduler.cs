using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiedye.Z80Core;

namespace Tiedye.Hardware
{
    public class Scheduler
    {
        public string GetDebugInformation()
        {
            StringBuilder str = new StringBuilder();
            str.Append("System clock cycles: ");
            str.AppendLine(clock.ClockTime.ToString());
            str.Append("System wall time: ");
            str.AppendLine(clock.WallTime.ToString());
            str.AppendLine();
            str.AppendLine("Wall-time queue:");
            int wtSize = 0;
            if (wtHead.Next != null)
                for (WallTimeEvent wt = wtHead.Next; wt != null; wt = wt.Next)
                {
                    wtSize++;
                    str.Append(wtSize);
                    str.Append(": ");
                    if (wt.Tag != null)
                        str.Append(wt.Tag);
                    else
                        str.Append("<no tag>");
                    str.Append(" @ ");
                    str.AppendLine(wt.Time.ToString());
                }
            else
                str.AppendLine("(No items.)");
            str.Append("Total pending wall-time events: ");
            str.AppendLine(wtSize.ToString());
            str.AppendLine();
            str.AppendLine("Clock-time queue:");
            int scSize = 0;
            if (scHead.Next != null)
                for (SystemClockEvent sc = scHead.Next; sc.Next != null; sc = sc.Next)
                {
                    scSize++;
                    str.Append(scSize);
                    str.Append(": ");
                    if (sc.Tag != null)
                        str.Append(sc.Tag);
                    else
                        str.Append("<no tag>");
                    str.Append(" @ ");
                    str.AppendLine(sc.Time.ToString());
                }
            else
                str.AppendLine("(No items.)");
            str.Append("Total pending system clock events: ");
            str.AppendLine(scSize.ToString());
            return str.ToString();
        }

        SystemClock clock;
        public Scheduler(SystemClock c)
        {
            clock = c;
        }

        public class WallTimeEvent
        {
            public double Time;
            public object Attachment;
            public EventHandler<WallTimeEvent> Handler;
            public string Tag;
            internal WallTimeEvent Next;
        }

        private WallTimeEvent wtHead = new WallTimeEvent();

        public void EnqueueEvent(WallTimeEvent e)
        {
            if (wtHead.Next == null)
            {
                wtHead.Next = e;
                e.Next = null;
            }
            else
            {
                //WallTimeEvent node = wtHead;
                WallTimeEvent node;
                for (node = wtHead; node.Next != null && e.Time > node.Next.Time; node = node.Next)
                    ; // Just iterate
                e.Next = node.Next;
                node.Next = e;
            }
        }

        public void RemoveEvent(WallTimeEvent e)
        {
            if (wtHead.Next == null)
                return;
            for (WallTimeEvent i = wtHead; i.Next != null; i = i.Next)
            {
                if (i.Next == e)
                {
                    i.Next = e.Next;
                    break;
                }
            }
        }

        public void RemoveEvent(SystemClockEvent e)
        {
            if (wtHead.Next == null)
                return;
            for (SystemClockEvent i = scHead; i.Next != null; i = i.Next)
            {
                if (i.Next == e)
                {
                    i.Next = e.Next;
                    break;
                }
            }
        }

        public void EnqueueEvent(WallTimeEvent e, double time)
        {
            e.Time = time;
            EnqueueEvent(e);
        }

        public void EnqueueRelativeEvent(WallTimeEvent e, double delta)
        {
            EnqueueEvent(e, clock.WallTime + delta);
        }


        public class SystemClockEvent
        {
            public long Time;
            public object Attachment;
            public EventHandler<SystemClockEvent> Handler;
            public string Tag;
            internal SystemClockEvent Next;
        }

        private SystemClockEvent scHead = new SystemClockEvent();


        public void EnqueueEvent(SystemClockEvent e)
        {
            if (scHead.Next == null)
            {
                scHead.Next = e;
                e.Next = null;
            }
            else
            {
                SystemClockEvent node;
                for (node = scHead; node.Next != null && e.Time > node.Next.Time; node = node.Next)
                    ; // Just iterate
                e.Next = node.Next;
                node.Next = e;
            }
        }

        public void EnqueueEvent(SystemClockEvent e, long time)
        {
            e.Time = time;
            EnqueueEvent(e);
        }

        public void EnqueueRelativeEvent(SystemClockEvent e, long delta)
        {
            EnqueueEvent(e, clock.ClockTime + delta);
        }
        
        internal void ProcessEvents()
        {
            /*bool repeat = false;
            do
            {
                if (wtHead.Next != null)
                {
                    if (wtHead.Next.Time)
                }

            } while (repeat);*/

            while (TryReleaseWtEvent() || TryReleaseScEvent())
                ;
        }

        bool TryReleaseWtEvent()
        {
            if (wtHead.Next != null && clock.WallTime >= wtHead.Next.Time)
            {
                WallTimeEvent n = wtHead.Next;
                wtHead.Next = n.Next;
                if (n.Handler != null)
                    n.Handler(this, n);
                return true;
            }
            return false;
        }

        bool TryReleaseScEvent()
        {
            if (scHead.Next != null && clock.WallTime >= scHead.Next.Time)
            {
                SystemClockEvent n = scHead.Next;
                scHead.Next = n.Next;
                if (n.Handler != null)
                    n.Handler(this, n);
                return true;
            }
            return false;
        }
    }
}
