using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSMO;

public class ChannelService
{
    private List<Channel> channels;
    private int countParalelChannels;

    public delegate void UpdateHandler();
    public event UpdateHandler? UpdateChannel;

    public ChannelService(int channelsCount, int countParalelChannels)
    {
        channels = new(channelsCount);
        this.countParalelChannels = countParalelChannels;
    }

}
