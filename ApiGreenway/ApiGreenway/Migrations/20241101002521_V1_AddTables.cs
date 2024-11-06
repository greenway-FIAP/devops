using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiGreenway.Migrations
{
    /// <inheritdoc />
    public partial class V1_AddTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_GRW_ADDRESS",
                columns: table => new
                {
                    id_address = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_street = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ds_zip_code = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false),
                    ds_number = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ds_uf = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: false),
                    ds_neighborhood = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ds_city = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    dt_created_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    dt_updated_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    dt_finished_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    id_company = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRW_ADDRESS", x => x.id_address);
                });

            migrationBuilder.CreateTable(
                name: "T_GRW_BADGE",
                columns: table => new
                {
                    id_badge = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    tx_description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ds_criteria = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    st_badge = table.Column<int>(type: "NUMBER(10)", maxLength: 1, nullable: false),
                    url_image = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    dt_created_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    dt_updated_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    dt_finished_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    id_badge_level = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    id_sustainable_goal = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRW_BADGE", x => x.id_badge);
                });

            migrationBuilder.CreateTable(
                name: "T_GRW_BADGE_LEVEL",
                columns: table => new
                {
                    id_badge_level = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    tx_description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    dt_created_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    dt_updated_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    dt_finished_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRW_BADGE_LEVEL", x => x.id_badge_level);
                });

            migrationBuilder.CreateTable(
                name: "T_GRW_COMPANY",
                columns: table => new
                {
                    id_company = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    tx_description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    vl_current_revenue = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    nr_size = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    nr_cnpj = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    dt_created_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    dt_updated_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    dt_finished_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    id_sector = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    id_address = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRW_COMPANY", x => x.id_company);
                });

            migrationBuilder.CreateTable(
                name: "T_GRW_COMPANY_REPRESENTATIVE",
                columns: table => new
                {
                    id_company_representative = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ds_role = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    nr_phone = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    dt_created_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    dt_updated_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    dt_finished_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    id_user = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    id_company = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRW_COMPANY_REPRESENTATIVE", x => x.id_company_representative);
                });

            migrationBuilder.CreateTable(
                name: "T_GRW_IMPROVEMENT_MEASUREMENT",
                columns: table => new
                {
                    id_improvement_measurement = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    dt_created_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    dt_updated_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    dt_finished_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    id_sustainable_improvement_actions = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRW_IMPROVEMENT_MEASUREMENT", x => x.id_improvement_measurement);
                });

            migrationBuilder.CreateTable(
                name: "T_GRW_MEASUREMENT",
                columns: table => new
                {
                    id_measurement = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    tx_description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    dt_created_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    dt_updated_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    dt_finished_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    id_measurement_type = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    id_improvement_measurement = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    id_sustainable_goal = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRW_MEASUREMENT", x => x.id_measurement);
                });

            migrationBuilder.CreateTable(
                name: "T_GRW_MEASUREMENT_PROCESS_STEP",
                columns: table => new
                {
                    id_measurement_process_step = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nr_result = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    dt_created_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    dt_updated_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    dt_finished_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    id_measurement = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    id_process_step = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRW_MEASUREMENT_PROCESS_STEP", x => x.id_measurement_process_step);
                });

            migrationBuilder.CreateTable(
                name: "T_GRW_MEASUREMENT_TYPE",
                columns: table => new
                {
                    id_measurement_type = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_name = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    tx_description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    dt_created_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    dt_updated_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    dt_finished_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRW_MEASUREMENT_TYPE", x => x.id_measurement_type);
                });

            migrationBuilder.CreateTable(
                name: "T_GRW_PROCESS",
                columns: table => new
                {
                    id_process = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    st_process = table.Column<int>(type: "NUMBER(10)", maxLength: 1, nullable: false),
                    nr_priority = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    dt_start = table.Column<string>(type: "NVARCHAR2(10)", nullable: false),
                    dt_end = table.Column<string>(type: "NVARCHAR2(10)", nullable: false),
                    tx_description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    tx_comments = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    dt_created_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    dt_updated_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    dt_finished_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    id_company = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    id_company_representative = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRW_PROCESS", x => x.id_process);
                });

            migrationBuilder.CreateTable(
                name: "T_GRW_PROCESS_BADGE",
                columns: table => new
                {
                    id_process_badge = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    dt_expiration_date = table.Column<string>(type: "NVARCHAR2(10)", nullable: false),
                    url_badge = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    dt_created_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    dt_updated_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    dt_finished_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    id_process = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    id_badge = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRW_PROCESS_BADGE", x => x.id_process_badge);
                });

            migrationBuilder.CreateTable(
                name: "T_GRW_PROCESS_RESOURCE",
                columns: table => new
                {
                    id_process_resource = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    dt_created_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    dt_updated_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    dt_finished_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    id_resource = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    id_process = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRW_PROCESS_RESOURCE", x => x.id_process_resource);
                });

            migrationBuilder.CreateTable(
                name: "T_GRW_PROCESS_STEP",
                columns: table => new
                {
                    id_process_step = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    dt_created_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    dt_updated_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    dt_finished_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    id_step = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    id_process = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRW_PROCESS_STEP", x => x.id_process_step);
                });

            migrationBuilder.CreateTable(
                name: "T_GRW_PRODUCT",
                columns: table => new
                {
                    id_product = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    tx_description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    vl_sale_price = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    vl_cost_price = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    nr_weight = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    dt_created_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    dt_updated_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    dt_finished_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    id_product_type = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRW_PRODUCT", x => x.id_product);
                });

            migrationBuilder.CreateTable(
                name: "T_GRW_PRODUCT_TYPE",
                columns: table => new
                {
                    id_product_type = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    tx_description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    dt_created_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    dt_updated_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    dt_finished_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRW_PRODUCT_TYPE", x => x.id_product_type);
                });

            migrationBuilder.CreateTable(
                name: "T_GRW_RESOURCE",
                columns: table => new
                {
                    id_resource = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    tx_description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    vl_cost_per_unity = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    ds_unit_of_measurement = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    nr_availability = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    dt_created_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    dt_updated_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    dt_finished_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    id_resource_type = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRW_RESOURCE", x => x.id_resource);
                });

            migrationBuilder.CreateTable(
                name: "T_GRW_RESOURCE_TYPE",
                columns: table => new
                {
                    id_resource_type = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    tx_description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    dt_created_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    dt_updated_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    dt_finished_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRW_RESOURCE_TYPE", x => x.id_resource_type);
                });

            migrationBuilder.CreateTable(
                name: "T_GRW_SECTOR",
                columns: table => new
                {
                    id_sector = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    tx_description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    dt_created_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    dt_updated_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    dt_finished_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRW_SECTOR", x => x.id_sector);
                });

            migrationBuilder.CreateTable(
                name: "T_GRW_STEP",
                columns: table => new
                {
                    id_step = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    tx_description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    nr_estimated_time = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    st_step = table.Column<int>(type: "NUMBER(10)", maxLength: 1, nullable: false),
                    dt_start = table.Column<string>(type: "NVARCHAR2(10)", nullable: false),
                    dt_end = table.Column<string>(type: "NVARCHAR2(10)", nullable: false),
                    dt_created_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    dt_updated_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    dt_finished_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRW_STEP", x => x.id_step);
                });

            migrationBuilder.CreateTable(
                name: "T_GRW_SUSTAINABLE_GOAL",
                columns: table => new
                {
                    id_sustainable_goal = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    tx_description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    vl_target = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    dt_created_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    dt_updated_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    dt_finished_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    id_badge = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRW_SUSTAINABLE_GOAL", x => x.id_sustainable_goal);
                });

            migrationBuilder.CreateTable(
                name: "T_GRW_SUSTAINABLE_IMPROVEMENT_ACTIONS",
                columns: table => new
                {
                    id_sustainable_improvement_action = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    tx_instruction = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    st_sustainable_action = table.Column<int>(type: "NUMBER(10)", maxLength: 1, nullable: false),
                    nr_priority = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    dt_created_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    dt_updated_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    dt_finished_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    id_sustainable_goal = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRW_SUSTAINABLE_IMPROVEMENT_ACTIONS", x => x.id_sustainable_improvement_action);
                });

            migrationBuilder.CreateTable(
                name: "T_GRW_USER",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_uid_fb = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ds_email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ds_password = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    dt_created_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    dt_updated_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    dt_finished_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    id_user_type = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    id_company_representative = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRW_USER", x => x.id_user);
                });

            migrationBuilder.CreateTable(
                name: "T_GRW_USER_TYPE",
                columns: table => new
                {
                    id_user_type = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_title = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    dt_created_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    dt_updated_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    dt_finished_at = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GRW_USER_TYPE", x => x.id_user_type);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_GRW_ADDRESS");

            migrationBuilder.DropTable(
                name: "T_GRW_BADGE");

            migrationBuilder.DropTable(
                name: "T_GRW_BADGE_LEVEL");

            migrationBuilder.DropTable(
                name: "T_GRW_COMPANY");

            migrationBuilder.DropTable(
                name: "T_GRW_COMPANY_REPRESENTATIVE");

            migrationBuilder.DropTable(
                name: "T_GRW_IMPROVEMENT_MEASUREMENT");

            migrationBuilder.DropTable(
                name: "T_GRW_MEASUREMENT");

            migrationBuilder.DropTable(
                name: "T_GRW_MEASUREMENT_PROCESS_STEP");

            migrationBuilder.DropTable(
                name: "T_GRW_MEASUREMENT_TYPE");

            migrationBuilder.DropTable(
                name: "T_GRW_PROCESS");

            migrationBuilder.DropTable(
                name: "T_GRW_PROCESS_BADGE");

            migrationBuilder.DropTable(
                name: "T_GRW_PROCESS_RESOURCE");

            migrationBuilder.DropTable(
                name: "T_GRW_PROCESS_STEP");

            migrationBuilder.DropTable(
                name: "T_GRW_PRODUCT");

            migrationBuilder.DropTable(
                name: "T_GRW_PRODUCT_TYPE");

            migrationBuilder.DropTable(
                name: "T_GRW_RESOURCE");

            migrationBuilder.DropTable(
                name: "T_GRW_RESOURCE_TYPE");

            migrationBuilder.DropTable(
                name: "T_GRW_SECTOR");

            migrationBuilder.DropTable(
                name: "T_GRW_STEP");

            migrationBuilder.DropTable(
                name: "T_GRW_SUSTAINABLE_GOAL");

            migrationBuilder.DropTable(
                name: "T_GRW_SUSTAINABLE_IMPROVEMENT_ACTIONS");

            migrationBuilder.DropTable(
                name: "T_GRW_USER");

            migrationBuilder.DropTable(
                name: "T_GRW_USER_TYPE");
        }
    }
}
