using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using WC.Library.Web.Controllers;
using WC.Library.Web.Models;
using WC.Service.PersonalData.API.Models;
using WC.Service.PersonalData.Domain.Models;
using WC.Service.PersonalData.Domain.Services;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace WC.Service.PersonalData.API.Controllers;

/// <summary>
///     The personal data management controller.
/// </summary>
[Route("api/v1/personal-data")]
public class PersonalDataController
    : CrudApiControllerBase<PersonalDataController, IPersonalDataManager, IPersonalDataProvider, PersonalDataModel,
        PersonalDataDto>
{
    /// <inheritdoc/>
    public PersonalDataController(
        IMapper mapper,
        ILogger<PersonalDataController> logger,
        IPersonalDataManager manager,
        IPersonalDataProvider provider)
        : base(mapper, logger, manager, provider)
    {
    }

    /// <summary>
    ///     Retrieves a list of personal data.
    /// </summary>
    /// <param name="withIncludes">Specifies whether related entities should be included in the query.</param>
    /// <param name="cancellationToken">The operation cancellation token.</param>
    /// <returns></returns>
    [HttpGet]
    [OpenApiOperation(nameof(PersonalDataGet))]
    [SwaggerResponse(Status200OK, typeof(List<PersonalDataDto>))]
    public async Task<ActionResult<List<PersonalDataDto>>> PersonalDataGet(
        bool withIncludes = false,
        CancellationToken cancellationToken = default)
    {
        return Ok(await GetMany(withIncludes, cancellationToken: cancellationToken));
    }

    /// <summary>
    ///     Retrieves a personal data by its ID.
    /// </summary>
    /// <param name="id">The ID of the personal data to retrieve.</param>
    /// <param name="cancellationToken">The operation cancellation token.</param>
    /// <returns></returns>
    [HttpGet("{id:guid}", Name = nameof(PersonalDataGetById))]
    [OpenApiOperation(nameof(PersonalDataGetById))]
    [SwaggerResponse(Status200OK, typeof(PersonalDataDto))]
    [SwaggerResponse(Status404NotFound, typeof(ErrorDto))]
    public async Task<ActionResult<PersonalDataDto>> PersonalDataGetById(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return Ok(await GetOneById(id, true, cancellationToken: cancellationToken));
    }

    /// <summary>
    ///     Creates new personal data.
    /// </summary>
    /// <param name="payload">The personal data content.</param>
    /// <param name="cancellationToken">The operation cancellation token</param>
    /// <returns>The result of creation. <see cref="CreateActionResultDto"/></returns>
    [HttpPost]
    [OpenApiOperation(nameof(PersonalDataCreate))]
    [SwaggerResponse(Status201Created, typeof(CreateActionResultDto))]
    public Task<IActionResult> PersonalDataCreate(
        [FromBody] PersonalDataCreateDto payload,
        CancellationToken cancellationToken = default)
    {
        return Create<PersonalDataCreateDto, CreateActionResultDto>(payload, nameof(PersonalDataGetById),
            cancellationToken);
    }

    /// <summary>
    ///     Updates a personal data by ID.
    /// </summary>
    /// <param name="id">The ID of the personal data to update.</param>
    /// <param name="patchDocument">The JSON patch document containing updates.</param>
    /// <param name="cancellationToken">The operation cancellation token.</param>
    /// <returns></returns>
    [HttpPatch("{id:guid}")]
    [OpenApiOperation(nameof(PersonalDataUpdate))]
    [SwaggerResponse(Status200OK, typeof(void))]
    [SwaggerResponse(Status404NotFound, typeof(ErrorDto))]
    public async Task<IActionResult> PersonalDataUpdate(
        Guid id,
        [FromBody] JsonPatchDocument<PersonalDataUpdateDto> patchDocument,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Update(id, patchDocument, cancellationToken: cancellationToken));
    }

    /// <summary>
    ///     Deletes a personal data by ID.
    /// </summary>
    /// <param name="id">The ID of the personal data to delete.</param>
    /// <param name="cancellationToken">The operation cancellation token.</param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    [OpenApiOperation(nameof(PersonalDataDelete))]
    [SwaggerResponse(Status204NoContent, typeof(void))]
    [SwaggerResponse(Status404NotFound, typeof(ErrorDto))]
    [SwaggerResponse(Status409Conflict, typeof(ErrorDto))]
    public async Task<IActionResult> PersonalDataDelete(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await Delete(id, cancellationToken: cancellationToken);
    }
}
