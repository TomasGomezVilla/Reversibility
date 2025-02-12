let dataTable;
let instrumentsFilter = [];
let portfolioFilter = [];
let productTypeFilter = [];

/**
 * Function called in the GlobalSchedule.razor file to initialize the MultiSelect.
 * @param {{ value: string; text: String }[]} instrumentOptions Options for the instrument select.
 * @param {{ value: string; text: String }[]} portfolioOptions Options for the portfolio
 * @param {{ value: string; text: String }[]} productTypeOptions Options for the options
 */
export function initMultiSelect(portfolioOptions, instrumentOptions, productTypeOptions) {
    const instrumentElement = $("#instrument");
    const portfolioElement = $("#portfolio");
    const productTypeElement = $("#product-type");

    // Remove the form-control class to avoid styling issues.
    instrumentElement.removeClass("form-control");
    portfolioElement.removeClass("form-control");
    productTypeElement.removeClass("form-control");

    const strToArray = str => str.split(",").filter(Boolean);

    $(document).ready(() => {
        instrumentElement.selectize({
            persist: false,
            options: instrumentOptions,
            
            // Example of the objective "CFS Infra, CFS..." -> ["CFS Infra", "CFS", ...]
            onChange: (options) => {
                instrumentsFilter = strToArray(options);
            }
        });

        portfolioElement.selectize({
            persist: false,
            options: portfolioOptions,
            onChange: (options) => {
                portfolioFilter = strToArray(options);
            }
        });

        productTypeElement.selectize({
            persist: false,
            options: productTypeOptions,
            onChange: (options) => {
                productTypeFilter = strToArray(options);
            }
        });
    });
}

export const handleFetchClick = (instanceReference) => {
    const launchButton = $("#launch-button");
    
    const maturityDateStart = $("#maturity-date-start").val();
    const maturityDateEnd = $("#maturity-date-end").val();

    launchButton.prop("disabled", true);
    launchButton.text("Loading...");
    
    instanceReference.invokeMethodAsync(
        "LoadGlobalSchedule", /// Research further...
        instrumentsFilter,
        portfolioFilter,
        productTypeFilter,
        maturityDateStart,
        maturityDateEnd
    )
        .then((data) => {
            // data: string[][]
            
            // Clear the DataTable before adding new data.
            dataTable.clear().draw();

            dataTable.rows.add(data).draw();

            launchButton.prop("disabled", false);
            launchButton.text("Launch");
        })
        .error((error) => {
            // TODO: Handle error
            console.error(error);

            launchButton.prop("disabled", false);
            launchButton.text("Launch");
        })
}

/**
 * Function called in the GlobalSchedule.razor file to initialize the DataTable.
 */
export function init() {
    /**
     * This function ensures that all the scripts (external libraries, e.g. jQuery) and the DOM (Document Object Model is a programming interface that represents the structure of a webpage) are fully loaded before executing the code inside it.
     *
     * If we don't wait for the DOM to be fully loaded, DataTable class will be not available in the Global scope.
     */
    $(document).ready(() => {
        dataTable = $("#global-schedule-table").DataTable({
            searchable: true,
            responsive: true,
            sortable: true,
            perPage: 10,
            perPageSelect: false,
            layout: {
                topStart: {
                    buttons: ['excel']
                }
            },
            labels: {
                placeholder: "Search...",
                perPage: "{select} entries per page",
                noRows: "No trades found",
                info: "Showing {start} to {end} of {rows} Global schedule"
            }
        });
    });
}