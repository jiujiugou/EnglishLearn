using DomainCommons.Models;
using Listening.Domain.Event;
using Listening.Domain.Subtitles;
using Listening.Domain.ValueObjets;

namespace Listening.Domain.Entities
{
    public record Episode : AggregateRootEntity, IAggregateRoot
    {
        private Episode() { }
        public int SequenceNumber { get; private set; }//序号
        public MultilingualString Name { get; private set; }//标题
        public Guid AlbumId { get; private set; }//专辑Id，因为Episode和Album都是聚合根，因此不能直接做对象引用。
        public Uri AudioUrl { get; private set; }//音频路径

        public double DurationInSecond { get; private set; }//音频时长（秒数）

        //因为启用了<Nullable>enable</Nullable>，所以string是不可空，Migration会默认这个，string?是可空
        public string Subtitle { get; private set; }//原文字幕内容
        public string SubtitleType { get; private set; }//原文字幕格式

        /// <summary>
        /// 用户是否可见（如果发现内部有问题，就先隐藏）
        /// </summary>
        public bool IsVisible { get; private set; }
        

        public Episode ChangeSequenceNumber(int value)
        {
            this.SequenceNumber = value;
            this.AddDomainEventIfAbsent(new EpisodeUpdatedEvent(this));
            return this;
        }

        public Episode ChangeName(MultilingualString value)
        {
            this.Name = value;
            this.AddDomainEventIfAbsent(new EpisodeUpdatedEvent(this));
            return this;
        }

        public Episode ChangeSubtitle(string subtitleType, string subtitle)
        {
            var parser = SubtitleParserFactory.GetParser(subtitleType);
            if (parser == null)
            {
                throw new ArgumentOutOfRangeException(nameof(subtitleType), $"subtitleType={subtitleType} is not supported.");
            }
            this.SubtitleType = subtitleType;
            this.Subtitle = subtitle;
            this.AddDomainEventIfAbsent(new EpisodeUpdatedEvent(this));
            return this;
        }

        public Episode Hide()
        {
            this.IsVisible = false;
            this.AddDomainEventIfAbsent(new EpisodeUpdatedEvent(this));
            return this;
        }
        public Episode Show()
        {
            this.IsVisible = true;
            this.AddDomainEventIfAbsent(new EpisodeUpdatedEvent(this));
            return this;
        }

        public override void SoftDelete()
        {
            base.SoftDelete();
            this.AddDomainEvent(new EpisodeDeletedEvent(this.Id));
        }

        public IEnumerable<Sentence> ParseSubtitle()
        {
            var parser = SubtitleParserFactory.GetParser(this.SubtitleType);
            return parser.Parse(this.Subtitle);
        }
        public class Builder
        {
            private Guid id;
            private int sequenceNumber;
            private MultilingualString name;
            private Guid albumId;
            private Uri audioUrl;
            private double durationInSecond;
            private string subtitle;
            private string subtitleType;
            public Builder Id(Guid value)
            {
                this.id = value;
                return this;
            }
            public Builder SequenceNumber(int value)
            {
                this.sequenceNumber = value;
                return this;
            }
            public Builder Name(MultilingualString value)
            {
                this.name = value;
                return this;
            }
            public Builder AlbumId(Guid value)
            {
                this.albumId = value;
                return this;
            }
            public Builder AudioUrl(Uri value)
            {
                this.audioUrl = value;
                return this;
            }
            public Builder DurationInSecond(double value)
            {
                this.durationInSecond = value;
                return this;
            }
            public Builder Subtitle(string value)
            {
                this.subtitle = value;
                return this;
            }
            public Builder SubtitleType(string value)
            {
                this.subtitleType = value;
                return this;
            }
            public Episode Build()
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentOutOfRangeException(nameof(id));
                }
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                if (albumId == Guid.Empty)
                {
                    throw new ArgumentOutOfRangeException(nameof(albumId));
                }
                if (audioUrl == null)
                {
                    throw new ArgumentNullException(nameof(audioUrl));
                }
                if (durationInSecond <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(durationInSecond));
                }
                if (subtitle == null)
                {
                    throw new ArgumentNullException(nameof(subtitle));
                }
                if (subtitleType == null)
                {
                    throw new ArgumentNullException(nameof(subtitleType));
                }
                Episode e = new Episode();
                e.Id = id;
                e.SequenceNumber = sequenceNumber;
                e.Name = name;
                e.AlbumId = albumId;
                e.AudioUrl = audioUrl;
                e.DurationInSecond = durationInSecond;
                e.Subtitle = subtitle;
                e.SubtitleType = subtitleType;
                e.IsVisible = true;
                e.AddDomainEvent(new EpisodeCreatedEvent(e));
                return e;
            }
        }
    }
}
