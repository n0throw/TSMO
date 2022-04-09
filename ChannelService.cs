using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSMO;

public class ChannelService
{
    private List<Channel> channels;
    private Dictionary<int, double> requestsProcTime;
    private int countParalelChannels;

    public ChannelService(int channelsCount, int countParalelChannels)
    {
        channels = new(channelsCount);
        this.countParalelChannels = countParalelChannels;

        requestsProcTime = new();
    }

    public void AddRequest(double activeTime, int indexRequest)
    {
        UpdateChannel(activeTime);

        List<Channel> temp = channels.Where(obj => !obj.IsActive).ToList();
        int countActive;

        if (temp.Count >= countParalelChannels)
            countActive = countParalelChannels;
        else
            countActive = temp.Count;

        for (int i = 0; i < countActive; i++)
        {
            temp[i] = temp[i] with
            {
                IsActive = true,
                IndexRequest = indexRequest,
                StartTime = activeTime
            };

            UpdateProcTime(activeTime, indexRequest, temp[i].ProcessingTime);
        }
    }

    private void UpdateProcTime(double activeTime, int indexRequest, double procTimeChannel)
    {
        foreach (KeyValuePair<int, double> proccTime in requestsProcTime.Where(procTime => !channels.Contains(new Channel() { IndexRequest = procTime.Key }, new ChannelComparerWithIndex())))
        {
            requestsProcTime.Remove(proccTime.Key);
        }

        if (requestsProcTime.ContainsKey(indexRequest))
        {
            requestsProcTime[indexRequest] -=
                (activeTime - requestsProcTime[indexRequest]) / channels.
                    Where(channel => channel.IndexRequest == indexRequest).
                    Count();
        }
        else
            requestsProcTime.Add(indexRequest, procTimeChannel);
    }

    private void UpdateChannel(double activeTime)
    {
        channels.ForEach(channel =>
        {
            if (channel.IsActive)
            {
                if (requestsProcTime[channel.IndexRequest.Value] + channel.StartTime <= activeTime)
                {
                    channel.IsActive = false;
                    channel.StartTime = null;
                    channel.IndexRequest = null;
                }
            }
        });
    }
}


