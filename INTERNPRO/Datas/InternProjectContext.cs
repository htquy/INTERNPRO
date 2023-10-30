using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace INTERNPRO.Datas;

public partial class InternProjectContext : DbContext
{
    public InternProjectContext()
    {
    }

    public InternProjectContext(DbContextOptions<InternProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Diem> Diems { get; set; }

    public virtual DbSet<GiaoVien> GiaoViens { get; set; }

    public virtual DbSet<HocSinh> HocSinhs { get; set; }

    public virtual DbSet<LopHoc> LopHocs { get; set; }

    public virtual DbSet<Luong> Luongs { get; set; }

    public virtual DbSet<MonHoc> MonHocs { get; set; }

    public virtual DbSet<PhanCongCt> PhanCongCts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-EEG1PQP\\SQLEXPRESS;Initial Catalog=InternProject;Integrated Security=True;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Diem>(entity =>
        {
            entity.HasKey(e => e.MaDiem);

            entity.ToTable("Diem");

            entity.Property(e => e.MaDiem).ValueGeneratedNever();
            entity.Property(e => e.DiemCk).HasColumnName("DiemCK");
            entity.Property(e => e.DiemGk).HasColumnName("DiemGK");
            entity.Property(e => e.Lop)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.MaHs).HasColumnName("MaHS");
            entity.Property(e => e.MaMh).HasColumnName("MaMH");

            entity.HasOne(d => d.MaHsNavigation).WithMany(p => p.Diems)
                .HasForeignKey(d => d.MaHs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Diem_HocSinh");

            entity.HasOne(d => d.MaMhNavigation).WithMany(p => p.Diems)
                .HasForeignKey(d => d.MaMh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Diem_MonHoc");
        });

        modelBuilder.Entity<GiaoVien>(entity =>
        {
            entity.HasKey(e => e.MaGv);

            entity.ToTable("GiaoVien");

            entity.Property(e => e.MaGv)
                .ValueGeneratedNever()
                .HasColumnName("MaGV");
            entity.Property(e => e.ChuNhiemLop)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.ChuyenMon).HasMaxLength(50);
            entity.Property(e => e.GioiTinh)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.NgayBatDau).HasColumnType("datetime");
            entity.Property(e => e.NgaySinh).HasColumnType("date");
            entity.Property(e => e.PassWord)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.QueQuan).HasMaxLength(50);
            entity.Property(e => e.SoDienThoaiGv)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("SoDienThoaiGV");
            entity.Property(e => e.TenGv)
                .HasMaxLength(50)
                .HasColumnName("TenGV");

            entity.HasOne(d => d.MaLuongNavigation).WithMany(p => p.GiaoViens)
                .HasForeignKey(d => d.MaLuong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GiaoVien_Luong1");
        });

        modelBuilder.Entity<HocSinh>(entity =>
        {
            entity.HasKey(e => e.MaHs);

            entity.ToTable("HocSinh");

            entity.Property(e => e.MaHs)
                .ValueGeneratedNever()
                .HasColumnName("MaHS");
            entity.Property(e => e.GioiTinh)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.HoTenHs)
                .HasMaxLength(50)
                .HasColumnName("HoTenHS");
            entity.Property(e => e.HoTenPh)
                .HasMaxLength(50)
                .HasColumnName("HoTenPH");
            entity.Property(e => e.NgayNh)
                .HasColumnType("date")
                .HasColumnName("NgayNH");
            entity.Property(e => e.NgaySinh).HasColumnType("date");
            entity.Property(e => e.PassWord)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.QueQuan).HasMaxLength(50);
            entity.Property(e => e.SoDienThoaiHs)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("SoDienThoaiHS");
            entity.Property(e => e.SoDienThoaiPh)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("SoDienThoaiPH");
            entity.Property(e => e.TenLop)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<LopHoc>(entity =>
        {
            entity.HasKey(e => e.TenLop);

            entity.ToTable("LopHoc");

            entity.Property(e => e.TenLop)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Gvcn)
                .HasMaxLength(50)
                .HasColumnName("GVCN");
        });

        modelBuilder.Entity<Luong>(entity =>
        {
            entity.HasKey(e => e.MaLuong);

            entity.ToTable("Luong");

            entity.Property(e => e.MaLuong).ValueGeneratedNever();
            entity.Property(e => e.MucLuongCb).HasColumnName("MucLuongCB");
        });

        modelBuilder.Entity<MonHoc>(entity =>
        {
            entity.HasKey(e => e.MaMh);

            entity.ToTable("MonHoc");

            entity.Property(e => e.MaMh)
                .ValueGeneratedNever()
                .HasColumnName("MaMH");
            entity.Property(e => e.TenMh)
                .HasMaxLength(50)
                .HasColumnName("TenMH");
        });

        modelBuilder.Entity<PhanCongCt>(entity =>
        {
            entity.HasKey(e => e.MaCt);

            entity.ToTable("PhanCongCT");

            entity.Property(e => e.MaCt)
                .ValueGeneratedNever()
                .HasColumnName("MaCT");
            entity.Property(e => e.MaGv).HasColumnName("MaGV");
            entity.Property(e => e.MaMh).HasColumnName("MaMH");
            entity.Property(e => e.TenLop)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.MaGvNavigation).WithMany(p => p.PhanCongCts)
                .HasForeignKey(d => d.MaGv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhanCongCT_GiaoVien");

            entity.HasOne(d => d.MaMhNavigation).WithMany(p => p.PhanCongCts)
                .HasForeignKey(d => d.MaMh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhanCongCT_MonHoc");

            entity.HasOne(d => d.TenLopNavigation).WithMany(p => p.PhanCongCts)
                .HasForeignKey(d => d.TenLop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhanCongCT_LopHoc");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
