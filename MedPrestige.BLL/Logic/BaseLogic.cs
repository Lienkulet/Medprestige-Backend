using AutoMapper;

namespace MedPrestige.BLL.Logic
{
    public abstract class BaseLogic<TEntity, TDto>
    {
        protected readonly IMapper _mapper;

        protected BaseLogic(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected TDto MapToDto(TEntity entity)
        {
            return _mapper.Map<TDto>(entity);
        }

        protected List<TDto> MapToDtoList(List<TEntity> entities)
        {
            return _mapper.Map<List<TDto>>(entities);
        }
    }
}
