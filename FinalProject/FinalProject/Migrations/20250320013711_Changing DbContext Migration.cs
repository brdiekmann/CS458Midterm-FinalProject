using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class ChangingDbContextMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attachments_Projects_ProjectId",
                table: "attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_biddingLog_Users_OperatorId",
                table: "biddingLog");

            migrationBuilder.DropForeignKey(
                name: "FK_biddingLog_projectBids_ProjectBidId",
                table: "biddingLog");

            migrationBuilder.DropForeignKey(
                name: "FK_projectBids_Projects_ProjectId",
                table: "projectBids");

            migrationBuilder.DropForeignKey(
                name: "FK_projectBids_Users_BidderId",
                table: "projectBids");

            migrationBuilder.DropForeignKey(
                name: "FK_projectLogs_Projects_ProjectId",
                table: "projectLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_projectLogs_Users_OperatorId",
                table: "projectLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_projectLogs",
                table: "projectLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_projectBids",
                table: "projectBids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_attachments",
                table: "attachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_biddingLog",
                table: "biddingLog");

            migrationBuilder.RenameTable(
                name: "projectLogs",
                newName: "ProjectLogs");

            migrationBuilder.RenameTable(
                name: "projectBids",
                newName: "ProjectBids");

            migrationBuilder.RenameTable(
                name: "attachments",
                newName: "Attachments");

            migrationBuilder.RenameTable(
                name: "biddingLog",
                newName: "BiddingLogs");

            migrationBuilder.RenameIndex(
                name: "IX_projectLogs_ProjectId",
                table: "ProjectLogs",
                newName: "IX_ProjectLogs_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_projectLogs_OperatorId",
                table: "ProjectLogs",
                newName: "IX_ProjectLogs_OperatorId");

            migrationBuilder.RenameIndex(
                name: "IX_projectBids_ProjectId",
                table: "ProjectBids",
                newName: "IX_ProjectBids_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_projectBids_BidderId",
                table: "ProjectBids",
                newName: "IX_ProjectBids_BidderId");

            migrationBuilder.RenameIndex(
                name: "IX_attachments_ProjectId",
                table: "Attachments",
                newName: "IX_Attachments_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_biddingLog_ProjectBidId",
                table: "BiddingLogs",
                newName: "IX_BiddingLogs_ProjectBidId");

            migrationBuilder.RenameIndex(
                name: "IX_biddingLog_OperatorId",
                table: "BiddingLogs",
                newName: "IX_BiddingLogs_OperatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectLogs",
                table: "ProjectLogs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectBids",
                table: "ProjectBids",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attachments",
                table: "Attachments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BiddingLogs",
                table: "BiddingLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Projects_ProjectId",
                table: "Attachments",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BiddingLogs_ProjectBids_ProjectBidId",
                table: "BiddingLogs",
                column: "ProjectBidId",
                principalTable: "ProjectBids",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BiddingLogs_Users_OperatorId",
                table: "BiddingLogs",
                column: "OperatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectBids_Projects_ProjectId",
                table: "ProjectBids",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectBids_Users_BidderId",
                table: "ProjectBids",
                column: "BidderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectLogs_Projects_ProjectId",
                table: "ProjectLogs",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectLogs_Users_OperatorId",
                table: "ProjectLogs",
                column: "OperatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Projects_ProjectId",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_BiddingLogs_ProjectBids_ProjectBidId",
                table: "BiddingLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_BiddingLogs_Users_OperatorId",
                table: "BiddingLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectBids_Projects_ProjectId",
                table: "ProjectBids");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectBids_Users_BidderId",
                table: "ProjectBids");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectLogs_Projects_ProjectId",
                table: "ProjectLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectLogs_Users_OperatorId",
                table: "ProjectLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectLogs",
                table: "ProjectLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectBids",
                table: "ProjectBids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attachments",
                table: "Attachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BiddingLogs",
                table: "BiddingLogs");

            migrationBuilder.RenameTable(
                name: "ProjectLogs",
                newName: "projectLogs");

            migrationBuilder.RenameTable(
                name: "ProjectBids",
                newName: "projectBids");

            migrationBuilder.RenameTable(
                name: "Attachments",
                newName: "attachments");

            migrationBuilder.RenameTable(
                name: "BiddingLogs",
                newName: "biddingLog");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectLogs_ProjectId",
                table: "projectLogs",
                newName: "IX_projectLogs_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectLogs_OperatorId",
                table: "projectLogs",
                newName: "IX_projectLogs_OperatorId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectBids_ProjectId",
                table: "projectBids",
                newName: "IX_projectBids_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectBids_BidderId",
                table: "projectBids",
                newName: "IX_projectBids_BidderId");

            migrationBuilder.RenameIndex(
                name: "IX_Attachments_ProjectId",
                table: "attachments",
                newName: "IX_attachments_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_BiddingLogs_ProjectBidId",
                table: "biddingLog",
                newName: "IX_biddingLog_ProjectBidId");

            migrationBuilder.RenameIndex(
                name: "IX_BiddingLogs_OperatorId",
                table: "biddingLog",
                newName: "IX_biddingLog_OperatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_projectLogs",
                table: "projectLogs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_projectBids",
                table: "projectBids",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_attachments",
                table: "attachments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_biddingLog",
                table: "biddingLog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_attachments_Projects_ProjectId",
                table: "attachments",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_biddingLog_Users_OperatorId",
                table: "biddingLog",
                column: "OperatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_biddingLog_projectBids_ProjectBidId",
                table: "biddingLog",
                column: "ProjectBidId",
                principalTable: "projectBids",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_projectBids_Projects_ProjectId",
                table: "projectBids",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_projectBids_Users_BidderId",
                table: "projectBids",
                column: "BidderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_projectLogs_Projects_ProjectId",
                table: "projectLogs",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_projectLogs_Users_OperatorId",
                table: "projectLogs",
                column: "OperatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
