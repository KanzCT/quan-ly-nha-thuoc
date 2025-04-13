using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuanLyThuoc.Data;

public partial class QuanlythuocContext : DbContext
{
    public QuanlythuocContext()
    {
    }

    public QuanlythuocContext(DbContextOptions<QuanlythuocContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }

    public virtual DbSet<DanhMuc> DanhMucs { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<Thuoc> Thuocs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietHoaDon>(entity =>
        {
            entity.HasKey(e => e.MaCt).HasName("PK__ChiTietH__27258E5432FEB0CB");

            entity.ToTable("ChiTietHoaDon");

            entity.Property(e => e.MaCt).ValueGeneratedNever();
            entity.Property(e => e.DonGia)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GiamGia).HasDefaultValue(0.0);
            entity.Property(e => e.SoLuong).HasDefaultValue(1);

            entity.HasOne(d => d.MaHdNavigation).WithMany(p => p.ChiTietHoaDons)
                .HasForeignKey(d => d.MaHd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietHoa__MaHd__4CA06362");

            entity.HasOne(d => d.MaThuocNavigation).WithMany(p => p.ChiTietHoaDons)
                .HasForeignKey(d => d.MaThuoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietHo__MaThu__4D94879B");
        });

        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.HasKey(e => e.MaDanhMuc).HasName("PK__DanhMuc__B37508873AAC3BB2");

            entity.ToTable("DanhMuc");

            entity.Property(e => e.TenDanhMuc).HasMaxLength(255);
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHd).HasName("PK__HoaDon__2725A6C069C25920");

            entity.ToTable("HoaDon");

            entity.Property(e => e.MaHd).ValueGeneratedNever();
            entity.Property(e => e.CachThanhToan).HasMaxLength(100);
            entity.Property(e => e.CachVanChuyen).HasMaxLength(100);
            entity.Property(e => e.DiaChi).HasMaxLength(500);
            entity.Property(e => e.DienThoai)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.GhiChu).HasMaxLength(1000);
            entity.Property(e => e.HoTen).HasMaxLength(255);
            entity.Property(e => e.MaKh)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NgayCan).HasColumnType("datetime");
            entity.Property(e => e.NgayDat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NgayGiao).HasColumnType("datetime");
            entity.Property(e => e.PhiVanChuyen)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaKh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaDon__MaKh__46E78A0C");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaDon__MaNv__45F365D3");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh).HasName("PK__KhachHan__2725CF7E3A8DC152");

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKh)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DiaChi).HasMaxLength(500);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.GioiTinh).HasDefaultValue(true);
            entity.Property(e => e.Hinh)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.RandomKey)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TenKh).HasMaxLength(255);
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNcc).HasName("PK__NhaCungC__3A1951E3468AAE08");

            entity.ToTable("NhaCungCap");

            entity.Property(e => e.MaNcc)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DiaChi).HasMaxLength(500);
            entity.Property(e => e.Gmail).HasMaxLength(255);
            entity.Property(e => e.Mota).HasMaxLength(1000);
            entity.Property(e => e.NguoiLienLac).HasMaxLength(255);
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TenCongTy).HasMaxLength(255);
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PK__NhanVien__2725D76AA3428785");

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gmail).HasMaxLength(255);
            entity.Property(e => e.Gt).HasMaxLength(10);
            entity.Property(e => e.HoTen).HasMaxLength(255);
            entity.Property(e => e.Luong).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Thuoc>(entity =>
        {
            entity.HasKey(e => e.MaThuoc).HasName("PK__Thuoc__4BB1F620E81DE00D");

            entity.ToTable("Thuoc");

            entity.Property(e => e.MaThuoc).ValueGeneratedNever();
            entity.Property(e => e.Dvt).HasMaxLength(50);
            entity.Property(e => e.GiaBan).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MaNcc)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MoTaNgan).HasMaxLength(500);
            entity.Property(e => e.TenThuoc).HasMaxLength(255);

            entity.HasOne(d => d.MaDanhMucNavigation).WithMany(p => p.Thuocs)
                .HasForeignKey(d => d.MaDanhMuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Thuoc__MaDanhMuc__3C69FB99");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.Thuocs)
                .HasForeignKey(d => d.MaNcc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Thuoc__MaNcc__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
