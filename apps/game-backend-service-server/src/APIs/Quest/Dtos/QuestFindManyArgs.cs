using GameBackendService.APIs.Common;
using GameBackendService.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameBackendService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class QuestFindManyArgs : FindManyInput<Quest, QuestWhereInput> { }
