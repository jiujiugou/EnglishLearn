using DomainCommons.Models;
using FluentValidation;
using Listening.Infrastructure;

namespace Listening.Admin.WebAPI.Episodes.Request
{
    public record EpisodeUpdateRequest(MultilingualString Name, string SubtitleType, string Subtitle);

    public class EpisodeUpdateRequestValidator : AbstractValidator<EpisodeUpdateRequest>
    {
        private ListeningDbContext ctx;
        public EpisodeUpdateRequestValidator(ListeningDbContext ctx)
        {
            this.ctx = ctx;
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name.Chinese).NotNull().Length(1, 200);
            RuleFor(x => x.Name.English).NotNull().Length(1, 200);
            RuleFor(x => x.SubtitleType).NotEmpty().Length(1, 10);
            RuleFor(x => x.Subtitle).NotEmpty().NotEmpty(); ;
        }
    }
}
