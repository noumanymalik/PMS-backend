using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Domain.Entities.Import;

namespace PMS.Persistence.ModelConfigurations
{
    internal class CallLogsConfiguration : IEntityTypeConfiguration<CallLogs>
    {
        public void Configure(EntityTypeBuilder<CallLogs> builder)
        {
            builder.Property(x => x.EmployeeId).HasColumnType("int");
            builder.Property(x => x.CreateDate);
            builder.Property(x => x.Category).HasMaxLength(20).IsRequired();
            builder.Property(x => x.FromPhoneNo).HasMaxLength(20).IsRequired();
            builder.Property(x => x.ToPhoneNo).HasMaxLength(80).IsRequired();
            builder.Property(x => x.InternetType).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.DispositionInternalId).HasColumnType("int");
            builder.Property(x => x.Disposition).HasMaxLength(50).IsRequired();
            builder.Property(x => x.FullRecording).HasMaxLength(500).IsRequired();
            builder.Property(x => x.QaAgentInternalId).HasColumnType("int");
            builder.Property(x => x.QaAgent).HasMaxLength(50).IsRequired();
            builder.Property(x => x.AgentNotes).HasMaxLength(500).IsRequired();
            builder.Property(x => x.OfferInternalId).HasColumnType("int");
            builder.Property(x => x.Offer).HasMaxLength(50).IsRequired();
            builder.Property(x => x.IaPowerDialerFlow).HasMaxLength(50).IsRequired();
            builder.Property(x => x.CallRouterInstantAgent).HasMaxLength(50).IsRequired();
            builder.Property(x => x.BuyerInternalId).HasColumnType("int");
            builder.Property(x => x.Buyer).HasMaxLength(50).IsRequired();
            builder.Property(x => x.AgentTime).HasColumnType("int");
            builder.Property(x => x.ForwardedTime).HasColumnType("decimal");
            builder.Property(x => x.HangupReason).HasMaxLength(50).IsRequired();
            builder.Property(x => x.HoldTime).HasColumnType("decimal");
            builder.Property(x => x.State).HasMaxLength(20).IsRequired();
        }
    }

    internal class CallSummaryAllConfiguration : IEntityTypeConfiguration<CallSummaryAll>
    {
        public void Configure(EntityTypeBuilder<CallSummaryAll> builder)
        {
            builder.Property(x => x.EmployeeId).HasColumnType("int");
            builder.Property(x => x.CreateDate);
            builder.Property(x => x.TotalCalls).HasColumnType("int");
            builder.Property(x => x.RegisteredTime).HasColumnType("int");
            builder.Property(x => x.AgentTimestampPausedBreak).HasColumnType("int");
            builder.Property(x => x.TimestampManualDial).HasColumnType("int");
            builder.Property(x => x.AgentTimestampTraining).HasColumnType("int");
            builder.Property(x => x.AgentTimestampWaitingForAgent).HasColumnType("int");
            builder.Property(x => x.AgentTimestampWaitingForDisposition).HasColumnType("int");
            builder.Property(x => x.BillableTotal).HasColumnType("int");
            builder.Property(x => x.UnbillableTotal).HasColumnType("int");
        }
    }

    internal class CallSummaryInboundConfiguration : IEntityTypeConfiguration<CallSummaryInbound>
    {
        public void Configure(EntityTypeBuilder<CallSummaryInbound> builder)
        {
            builder.Property(x => x.EmployeeId).HasColumnType("int");
            builder.Property(x => x.CreateDate);
            builder.Property(x => x.TotalCalls).HasColumnType("int");
            builder.Property(x => x.RegisteredTime).HasColumnType("int");
            builder.Property(x => x.AgentTimestampPausedBreak).HasColumnType("int");
            builder.Property(x => x.TimestampManualDial).HasColumnType("int");
            builder.Property(x => x.AgentTimestampTraining).HasColumnType("int");
            builder.Property(x => x.AgentTimestampWaitingForAgent).HasColumnType("int");
            builder.Property(x => x.AgentTimestampWaitingForDisposition).HasColumnType("int");
            builder.Property(x => x.BillableTotal).HasColumnType("int");
            builder.Property(x => x.UnbillableTotal).HasColumnType("int");
        }
    }

    internal class SalesInboundConfiguration : IEntityTypeConfiguration<Sales>
    {
        public void Configure(EntityTypeBuilder<Sales> builder)
        {
            builder.Property(x => x.EmployeeId).HasColumnType("int");
            builder.Property(x => x.CreateDate);
            builder.Property(x => x.CustomerName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.CallerId).HasMaxLength(20).IsRequired();
            builder.Property(x => x.OCN).HasMaxLength(50);
            builder.Property(x => x.Provider).HasMaxLength(20).IsRequired();
            builder.Property(x => x.RGU).HasColumnType("int");
            builder.Property(x => x.Portal).HasMaxLength(20).IsRequired();
        }
    }

    internal class ClientSalesConfiguration : IEntityTypeConfiguration<ClientSales>
    {
        public void Configure(EntityTypeBuilder<ClientSales> builder)
        {
            builder.Property(x => x.CreateDate);
            builder.Property(x => x.Date);
            builder.Property(x => x.EmployeeName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.InboundCalls).HasColumnType("int");
            builder.Property(x => x.InboundSales).HasColumnType("int");
            builder.Property(x => x.TMobileTransfer).HasColumnType("int");
            builder.Property(x => x.TMobileSales).HasColumnType("int");
            builder.Property(x => x.ATT_Transfer).HasColumnType("int");
            builder.Property(x => x.ATT_Sales).HasColumnType("int");
            builder.Property(x => x.SpectrumTransfer).HasColumnType("int");
            builder.Property(x => x.SpectrumSales).HasColumnType("int");
            builder.Property(x => x.SpanishTransfer).HasColumnType("int");
            builder.Property(x => x.SpanishSales).HasColumnType("int");
            builder.Property(x => x.ADTCalls).HasColumnType("int");
            builder.Property(x => x.TotalSales).HasColumnType("int");

        }
    }
}
