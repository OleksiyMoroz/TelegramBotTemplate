using System.ComponentModel.DataAnnotations;

namespace template.Infrastructure;

public class ChatStage
{
    [Key]
    public long ChatId { get; set; }
    public Stage Stage { get; set; }
    public int Step { get; set; }
    public string LanguageCode { get; set; }
}